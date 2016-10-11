using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wedding.Models.Upload
{
    public class UploadModel
    {
        public Guid UserId { get; set; }
        public HttpPostedFile File { get; set; }
    }

}