//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wedding.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class guest_booking
    {
        public int id { get; set; }
        public Nullable<int> guest_id { get; set; }
        public Nullable<bool> fri_sat { get; set; }
        public Nullable<bool> sat_sun { get; set; }
        public string booking_comment { get; set; }
    
        public virtual guest guest { get; set; }
    }
}