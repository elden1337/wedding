using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;

namespace Wedding.Models.Blog
{

    public class BlogIndexModel
    {

        public BlogIndexModel()
        {
            BlogItem = new List<BlogItem>();
        }

        public List<BlogItem> BlogItem { get; set; }
        public decimal? currentlat { get; set; }
        public decimal? currentlon { get; set; }
        public DateTime currentdate { get; set; }
    }

    public class BlogItem { 

        public int id { get; set; }
        public string title { get; set; }
        public string post { get; set; }
        public decimal? lat { get; set; }
        public decimal? lon { get; set; }
        public int? zoom { get; set; }
        public DateTime posted { get; set; }
    }

    public class BlogPostModel
    {
        public string title { get; set; }
        public string post { get; set; }
        public decimal lat { get; set; }
        public decimal lon { get; set; }
        public DateTime posted { get; set; }
    }
}