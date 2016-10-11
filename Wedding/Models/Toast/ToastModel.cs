using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wedding.Models.Toast
{
    public class IndexModel
    {
        public int Id { get; set; }
        public int thread_Id { get; set; }
        public int guest_id { get; set; }
        public timestamp added { get; set; }
        public ntext message { get; set; }
    }
}