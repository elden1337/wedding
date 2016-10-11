using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace Wedding.Controllers
{
        using Wedding.Data;
        using System.Security.Claims;
        using Wedding.Models.Car;
    


    public class CarController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LogIn", "Account");

            }

            var model = new ManageCarModel();

            ClaimsPrincipal principal = (ClaimsPrincipal)User;
            var userIdResult = principal.FindFirst(ClaimTypes.NameIdentifier);

            //List<Cars> tokens = new List<Cars>();

                        using (var db = new boda_liveEntities())
            {
                var carListQuery = from c in db.car
                                     join f1 in db.AspNetUsers on c.user_id equals f1.Id into f2
                                     from f in f2
                                   where c.free_seats_out > 0
                                     orderby c.id
                                     select new { c.id, c.free_seats_out, c.arrival_day, c.departure_time, c.home_address, c.home_lat, c.home_lon, f.Email};
                model.Cars.AddRange(
                    carListQuery.ToList()
                        .Select(
                            x =>
                            new ViewCarModel()
                                {
                                    Id = x.id,
                                    Seats = x.free_seats_out,
                                    Arrival_Day = x.arrival_day,
                                    Departure_time = x.departure_time,
                                    Home_Address = x.home_address,
                                    latitude = x.home_lat,
                                    longitude = x.home_lon,
                                    User = x.Email
                                }));
            }


            return View(model);

        }

        [HttpGet]
        public ActionResult AddCar()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LogIn", "Account");

            }

            var model = new ManageCarModel();

            ClaimsPrincipal principal = (ClaimsPrincipal)User;
            var userIdResult = principal.FindFirst(ClaimTypes.NameIdentifier);

            //List<Cars> tokens = new List<Cars>();

            using (var db = new Wedding.Data.boda_liveEntities())
            {
                var carListQuery = from c in db.car
                                   join f1 in db.AspNetUsers on c.user_id equals f1.Id into f2
                                   from f in f2
                                   orderby c.id
                                   select new { c.id, c.free_seats_out, c.arrival_day, c.departure_time, c.home_address, c.home_lat, c.home_lon, f.Email };
                model.Cars.AddRange(
                    carListQuery.ToList()
                        .Select(
                            x =>
                            new ViewCarModel()
                            {
                                Id = x.id,
                                Seats = x.free_seats_out,
                                Arrival_Day = x.arrival_day,
                                Departure_time = x.departure_time,
                                Home_Address = x.home_address,
                                latitude = x.home_lat,
                                longitude = x.home_lon,
                                User = x.Email
                            }));
            }


            return View(model);

        }
        //Denna är för att posta från nybilsformuläret
        [HttpPost]
        public ActionResult AddCar(ManageCarModel model)
        {
            if (model == null)
            {
                return this.HttpNotFound("Failed to build model");
            }

            using (var db = new Wedding.Data.boda_liveEntities())
            {
                ClaimsPrincipal principal = (ClaimsPrincipal)User;

                var userIdResult = principal.FindFirst(ClaimTypes.NameIdentifier);

                //var hasAccessQuery = from tokens in db.UserTokens
                //                     where tokens.UserId == userIdResult.Value && tokens.Token == model.Token
                //                     select tokens.Token;

                //if (!hasAccessQuery.Any())
                //{
                //    throw new InvalidOperationException("You failed to comply, I'm failing");
                //}

                foreach (var gg in model.AddCar ?? new List<AddCarModel>())
                {
                    var car = db.car.FirstOrDefault(x => x.id == gg.Id);
                    if (car == null)
                    {
                        car = new car();
                        
                    }
                    //if (guest == null)
                    //{
                    //    throw new Exception("No car added");
                    //}

                    car.free_seats_out = gg.Seats;
                    car.arrival_day = gg.Arrival_Day;
                    car.departure_time = gg.Departure_time;
                    car.home_address = gg.Home_Address;
                    car.home_lat = gg.latitude;
                    car.home_lon = gg.longitude;

                }

                db.SaveChanges();
            }

            return RedirectToAction("Index", "Car");
        }

    }
}
