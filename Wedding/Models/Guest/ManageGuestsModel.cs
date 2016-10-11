using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wedding.Models.Guest
{
    public class ManageGuestsPostModel
    {
        public ManageGuestsPostModel()
        {
            Guests = new List<ManageGuestItemModel>();
            ComingGuests = new List<ViewGuestComingModel>();
            GuestStatComing = new List<GuestStatComingModel>();
            GuestStatNotComing = new List<GuestStatNotComingModel>();
            GuestStatUndecided = new List<GuestStatUndecidedModel>();
            GuestStatFriSat = new List<GuestStatFriSatModel>();
            GuestStatSatSun = new List<GuestStatSatSunModel>();
        }

        public string Token { get; set; }

        public List<ManageGuestItemModel> Guests { get; set; }
        public List<ViewGuestComingModel> ComingGuests { get; set; }
        public List<GuestStatComingModel> GuestStatComing { get; set; }
        public List<GuestStatNotComingModel> GuestStatNotComing { get; set; }
        public List<GuestStatUndecidedModel> GuestStatUndecided { get; set; }
        public List<GuestStatFriSatModel> GuestStatFriSat { get; set; }
        public List<GuestStatSatSunModel> GuestStatSatSun { get; set; }
    }

    public class ManageGuestItemModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public bool Coming { get; set; }

        public DateTime Updated { get; set; }

        public bool VegAndFish { get; set; }

        public bool Veg { get; set; }

        public bool Vegan { get; set; }

        public bool LactoseIntollerant { get; set; }

        public bool GlutenIntollerant { get; set; }

        public bool NutAllergy { get; set; }

        public bool BringsCake { get; set; }

        public bool NonAlco { get; set; }

        public string FoodComment { get; set; }

        public bool FriSat { get; set; }

        public bool SatSun { get; set; }

        public string BookingComment { get; set; }
    }

    public class ManageGuestsModel : ManageGuestsPostModel
    {
        public ManageGuestsModel(string token)
        {
            Token = token;
        }
    }

    public class ViewGuestComingModel
    {
        public string LoginToken { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public bool Coming { get; set; }

        public DateTime Updated { get; set; }

        public bool VegAndFish { get; set; }

        public bool Veg { get; set; }

        public bool Vegan { get; set; }

        public bool LactoseIntollerant { get; set; }

        public bool GlutenIntollerant { get; set; }

        public bool NutAllergy { get; set; }

        public bool NonAlco { get; set; }

        public bool BringsCake { get; set; }

        public string FoodComment { get; set; }

        public bool FriSat { get; set; }

        public bool SatSun { get; set; }

        public string BookingComment { get; set; }
    }

    public class GuestStatComingModel
    {
        public int Id { get; set; }
    }

    public class GuestStatNotComingModel
    {
        public int Id { get; set; }
    }

    public class GuestStatUndecidedModel
    {
        public int Id { get; set; }
    }

    public class GuestStatFriSatModel
    {
        public int Guest_id { get; set; }
    }

    public class GuestStatSatSunModel
    {
        public int Guest_id { get; set; }
    }


}