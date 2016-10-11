using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;

namespace Wedding.Controllers
{
    using System.Security.Claims;
    using Wedding.Data;
    using Wedding.Models.Guest;

    public class GuestController : Controller
    {
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LogIn", "Account");

            }

            ClaimsPrincipal principal = (ClaimsPrincipal)User;
            var userIdResult = principal.FindFirst(ClaimTypes.NameIdentifier);

            List<TokensAndGuests> tokens = new List<TokensAndGuests>();

            using (var db = new boda_liveEntities())
            {
                var result = from managedToken in db.UserTokens
                             where userIdResult.Value == managedToken.UserId
                             join g in db.guest on managedToken.Token equals g.login_token
                             group g by g.login_token
                                 into subgrp
                                 select new TokensAndGuests() { Token = subgrp.Key };

                tokens.AddRange(result);
            }

            var model = new GuestIndexModel() { ManagedTokens = tokens };

            return View("Index", model);
            //return RedirectToAction("ManageGuests", new { token =  (model.ManagedTokens.FirstOrDefault())} );

        }

        [HttpPost]
        public ActionResult AddToken(string token)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return this.View("AskToLogin");
            }

            ClaimsPrincipal principal = (ClaimsPrincipal)User;

            var userIdResult = principal.FindFirst(ClaimTypes.NameIdentifier);

            using (var db = new Wedding.Data.boda_liveEntities())
            {
                var tokenExistQuery = from g in db.guest where token == g.login_token select g.id;
                if (!tokenExistQuery.Any())
                {
                    return this.Index();
                }

                var query = from tokenMap in db.UserTokens where tokenMap.UserId == userIdResult.Value && tokenMap.Token == token select tokenMap;

                var item = query.FirstOrDefault();

                if (item == null)
                {
                    UserTokens newData = new UserTokens();
                    newData.Token = token;
                    newData.UserId = userIdResult.Value;

                    db.UserTokens.Add(newData);
                    db.SaveChanges();
                    //return Redirect("Index");
                    //Redirect to guest-form after adding token
                    return RedirectToAction("ManageGuests", new { token = newData.Token });

                }
                else
                {
                    return this.Index();
                }
            }
        }

        [HttpGet]
        public ActionResult ManageGuests(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index");
            }

            var model = new ManageGuestsModel(token);

            using (var db = new Wedding.Data.boda_liveEntities())
            {
                var guestListQuery = from g in db.guest
                                     join f1 in db.food on g.id equals f1.guest_id into f2
                                     from f in f2.DefaultIfEmpty()
                                     join gb1 in db.guest_booking on g.id equals gb1.guest_id into gb2
                                     from gb in gb2.DefaultIfEmpty()
                                     where g.login_token == token
                                     orderby g.id
                                     select new { g.id, g.name, g.coming, g.email, g.phonenumber, g.updated, f.lactose, f.vegan, f.vegetarian, f.pescetarian, f.gluten, f.nut, f.nonalco, f.bringscake, f.food_comment, gb.fri_sat, gb.sat_sun, gb.booking_comment};
                model.Guests.AddRange(
                    guestListQuery.ToList()
                        .Select(
                            x =>
                            new ManageGuestItemModel()
                                {
                                    Coming = x.coming ?? true,
                                    Email = x.email,
                                    Id = x.id,
                                    Name = x.name,
                                    Phone = x.phonenumber,
                                    Updated = x.updated ?? DateTime.Now,
                                    LactoseIntollerant = x.lactose ?? false,
                                    Vegan = x.vegan ?? false,
                                    Veg = x.vegetarian ?? false,
                                    VegAndFish = x.pescetarian ?? false,
                                    GlutenIntollerant = x.gluten ?? false,
                                    NutAllergy = x.nut ?? false,
                                    NonAlco = x.nonalco ?? false, 
                                    BringsCake = x.bringscake ?? false,
                                    FriSat = x.fri_sat ?? false,
                                    SatSun = x.sat_sun ?? false,
                                    FoodComment = x.food_comment,
                                    BookingComment = x.booking_comment
                                }));
            }

            return this.View(model);
        }

        [HttpPost]
        public ActionResult ManageGuests(ManageGuestsPostModel model)
        {
            if (model == null)
            {
                return this.HttpNotFound("Failed to build model");
            }

            using (var db = new Wedding.Data.boda_liveEntities())
            {
                ClaimsPrincipal principal = (ClaimsPrincipal)User;

                var userIdResult = principal.FindFirst(ClaimTypes.NameIdentifier);

                var hasAccessQuery = from tokens in db.UserTokens
                                     where tokens.UserId == userIdResult.Value && tokens.Token == model.Token
                                     select tokens.Token;

                if (!hasAccessQuery.Any())
                {
                    throw new InvalidOperationException("You failed to comply, I'm failing");
                }

                foreach (var gg in model.Guests ?? new List<ManageGuestItemModel>())
                {
                    var guest = db.guest.FirstOrDefault(x => x.id == gg.Id);
                    if (guest == null)
                    {
                        throw new Exception("No guest found");
                    }

                    guest.coming = gg.Coming;
                    guest.updated = gg.Updated;
                    guest.email = gg.Email;
                    guest.phonenumber = gg.Phone;
                    guest.name = gg.Name;

                    var foodInfo = db.food.FirstOrDefault(x => x.guest_id == gg.Id);
                    if (foodInfo == null)
                    {
                        foodInfo = new food();
                        guest.food.Add(foodInfo);
                    }

                    foodInfo.guest_id = gg.Id;
                    foodInfo.lactose = gg.LactoseIntollerant;
                    foodInfo.pescetarian = gg.VegAndFish;
                    foodInfo.vegan = gg.Vegan;
                    foodInfo.vegetarian = gg.Veg;
                    foodInfo.nut = gg.NutAllergy;
                    foodInfo.gluten = gg.GlutenIntollerant;
                    foodInfo.food_comment = gg.FoodComment;
                    foodInfo.bringscake = gg.BringsCake;
                    foodInfo.nonalco = gg.NonAlco;

                    var bookingInfo = db.guest_booking.FirstOrDefault(x => x.guest_id == gg.Id);
                    if (bookingInfo == null)
                    {
                        bookingInfo = new guest_booking();
                        guest.guest_booking.Add(bookingInfo);
                    }

                    bookingInfo.guest_id = gg.Id;
                    bookingInfo.fri_sat = gg.FriSat;
                    bookingInfo.sat_sun = gg.SatSun;
                    bookingInfo.booking_comment = gg.BookingComment;
                }

                db.SaveChanges();
            }

            //return RedirectToAction("ManageGuests", new { token = model.Token });
            return RedirectToAction("Reciept", new { token = model.Token });
        }

        [HttpGet]
        public ActionResult Reciept(string token)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LogIn", "Account");

            }

            
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index");
            }

            var model = new ManageGuestsModel(token);

            using (var db = new Wedding.Data.boda_liveEntities())
            {
                var guestListQuery = from g in db.guest
                                     join f1 in db.food on g.id equals f1.guest_id into f2
                                     from f in f2.DefaultIfEmpty()
                                     join gb1 in db.guest_booking on g.id equals gb1.guest_id into gb2
                                     from gb in gb2.DefaultIfEmpty()
                                     where g.login_token == token
                                     orderby g.coming descending
                                     select new { g.id, g.name, g.coming, g.email, g.phonenumber, g.updated, f.lactose, f.vegan, f.vegetarian, f.pescetarian, f.gluten, f.nut, f.nonalco, f.bringscake, f.food_comment, gb.fri_sat, gb.sat_sun, gb.booking_comment };
                model.Guests.AddRange(
                    guestListQuery.ToList()
                        .Select(
                            x =>
                            new ManageGuestItemModel()
                            {
                                Coming = x.coming ?? true,
                                Email = x.email,
                                Id = x.id,
                                Name = x.name,
                                Phone = x.phonenumber,
                                Updated = x.updated ?? DateTime.Now,
                                LactoseIntollerant = x.lactose ?? false,
                                Vegan = x.vegan ?? false,
                                Veg = x.vegetarian ?? false,
                                VegAndFish = x.pescetarian ?? false,
                                GlutenIntollerant = x.gluten ?? false,
                                NutAllergy = x.nut ?? false,
                                NonAlco = x.nonalco ?? false,
                                BringsCake = x.bringscake ?? false,
                                FriSat = x.fri_sat ?? false,
                                SatSun = x.sat_sun ?? false,
                                FoodComment = x.food_comment,
                                BookingComment = x.booking_comment
                            }));
            }

            return this.View(model);
        }



        [HttpGet]
        public ActionResult ViewAllGuests(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index");
            }

            var Comingmodel = new ManageGuestsModel(token);

            using (var db = new Wedding.Data.boda_liveEntities())
            {
                var guestListQuery = from g in db.guest
                                     join f1 in db.food on g.id equals f1.guest_id into f2
                                     from f in f2.DefaultIfEmpty()
                                     join gb1 in db.guest_booking on g.id equals gb1.guest_id into gb2
                                     from gb in gb2.DefaultIfEmpty()
                                     orderby g.updated descending, g.login_token, g.id
                                     select new { g.name, g.coming, g.login_token, g.email, g.phonenumber, g.updated, f.nonalco, f.lactose, f.vegan, f.vegetarian, f.pescetarian, f.gluten, f.nut, f.bringscake, f.food_comment, gb.fri_sat, gb.sat_sun, gb.booking_comment };

                Comingmodel.ComingGuests.AddRange(
                    guestListQuery.ToList()
                        .Select(
                            x =>
                            new ViewGuestComingModel()
                            {
                                Coming = x.coming ?? false,
                                LoginToken = x.login_token,
                                Email = x.email,
                                Name = x.name,
                                Phone = x.phonenumber,
                                Updated = x.updated ?? DateTime.MinValue,
                                LactoseIntollerant = x.lactose ?? false,
                                Vegan = x.vegan ?? false,
                                Veg = x.vegetarian ?? false,
                                VegAndFish = x.pescetarian ?? false,
                                GlutenIntollerant = x.gluten ?? false,
                                NutAllergy = x.nut ?? false,
                                NonAlco = x.nonalco ?? false, 
                                BringsCake = x.bringscake ?? false,
                                FriSat = x.fri_sat ?? false,
                                SatSun = x.sat_sun ?? false,
                                FoodComment = x.food_comment,
                                BookingComment = x.booking_comment
                            }));


                var guestStatComingQuery = from g in db.guest
                                           where g.coming == true
                                     select new  { g.id  };

                Comingmodel.GuestStatComing.AddRange(
    guestStatComingQuery.ToList()
        .Select(
            x =>
            new GuestStatComingModel()
            {
               Id = x.id
            }));

                var guestStatNotComingQuery = from g in db.guest
                                           where g.coming == false
                                           select new { g.id };

                Comingmodel.GuestStatNotComing.AddRange(
    guestStatNotComingQuery.ToList()
        .Select(
            x =>
            new GuestStatNotComingModel()
            {
                Id = x.id
            }));

                var guestStatUndeterminedQuery = from g in db.guest
                                              where g.coming == null
                                              select new { g.id };

                Comingmodel.GuestStatUndecided.AddRange(
    guestStatUndeterminedQuery.ToList()
        .Select(
            x =>
            new GuestStatUndecidedModel()
            {
                Id = x.id
            }));

                var guestStatFriSatQuery = from gb in db.guest_booking
                                           join f1 in db.guest on gb.guest_id equals f1.id into f2
                                           from f in f2.DefaultIfEmpty()
                                           where gb.fri_sat == true && f.coming == true
                                                 select new { gb.id };

                Comingmodel.GuestStatFriSat.AddRange(
    guestStatFriSatQuery.ToList()
        .Select(
            x =>
            new GuestStatFriSatModel()
            {
                Guest_id = x.id
            }));

                var guestStatSatSunQuery = from gb in db.guest_booking
                                           join f1 in db.guest on gb.guest_id equals f1.id into f2
                                           from f in f2.DefaultIfEmpty()
                                           where gb.sat_sun == true && f.coming == true
                                           select new { gb.id };

                Comingmodel.GuestStatSatSun.AddRange(
    guestStatSatSunQuery.ToList()
        .Select(
            x =>
            new GuestStatSatSunModel()
            {
                Guest_id = x.id
            }));

            }

            return this.View(Comingmodel);

        }
    }
}



public static class MyExtensions
{
    public static string FormatBool(this HtmlHelper html, bool value)
    {
        return html.Encode(value ? "x" : "");
    }
}




