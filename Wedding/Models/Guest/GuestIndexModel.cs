using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wedding.Models.Guest
{
    public class GuestIndexModel
    {
        public GuestIndexModel()
        {
            ManagedTokens = new List<TokensAndGuests>();
        }

        public List<TokensAndGuests> ManagedTokens { get; set; }

      
    }
}