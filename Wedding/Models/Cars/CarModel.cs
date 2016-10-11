using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wedding.Models.Car
{
    public class ManageCarModel
    {
        public ManageCarModel()
        {
            Cars = new List<ViewCarModel>();
            AddCar = new List<AddCarModel>();

        }
            public List<ViewCarModel> Cars { get; set; }
            public List<AddCarModel> AddCar { get; set; }
        
        }

    public class ViewCarModel {

        public int Id {get; set; }
        
        public int Seats {get; set; }
        
        public string Arrival_Day {get; set; }

        public int Departure_time { get; set; }
        
        public string Home_Address {get; set; }
        
        public string latitude {get; set; }
        
        public string longitude {get; set; }
        
        public string User {get; set; }
        
    }

    public class AddCarModel
    {
        public int Id { get; set; }

        public int Seats { get; set; }

        public string Arrival_Day { get; set; }

        public int Departure_time { get; set; }

        public string Home_Address { get; set; }

        public string latitude { get; set; }

        public string longitude { get; set; }

        //public string User { get; set; }

    }


        //public List<TokensAndGuests> ManagedTokens { get; set; }

      
    }
