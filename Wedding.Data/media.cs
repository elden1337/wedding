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
    
    public partial class media
    {
        public string media_owner { get; set; }
        public string media_uri { get; set; }
        public System.DateTime uploaded { get; set; }
        public string filetype { get; set; }
        public Nullable<System.DateTime> exif_created { get; set; }
        public Nullable<int> exif_width { get; set; }
        public Nullable<int> exif_height { get; set; }
        public Nullable<decimal> exif_lat { get; set; }
        public Nullable<decimal> exif_lon { get; set; }
        public Nullable<bool> approved { get; set; }
        public Nullable<bool> deleted { get; set; }
        public int id { get; set; }
    }
}
