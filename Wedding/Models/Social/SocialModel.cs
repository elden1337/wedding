using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using Newtonsoft.Json;

namespace Wedding.Models.Social
{

    public class SocialIndexModel
    {

        public SocialIndexModel()
        {
            Blobs = new List<BlobListing>();

        }

        public List<BlobListing> Blobs { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public int Pages { get; set; }
        public int Currentpage { get; set; }
    }
    

    public class BlobListing
    {
        public string ImageUrl { get; set; }
        public string FileType { get; set; }
        public string MediaOwner { get; set; }
        public bool Deleted { get; set; }
        public bool Approved { get; set; }
        public int Id { get; set; }
        public DateTime? ExifCreated { get; set; }
    }

    public class UploadModel
    {
        public Guid UserId { get; set; }
        public HttpPostedFile File { get; set; }
    }

    public class ReCaptchaClass
    {
        public static string Validate(string EncodedResponse)
        {
            var client = new System.Net.WebClient();

            string PrivateKey = "6Le2AiMTAAAAABPEYOZdltWO96c6YDHh-pwnKaI0";

            var GoogleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", PrivateKey, EncodedResponse));

            var captchaResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ReCaptchaClass>(GoogleReply);

            return captchaResponse.Success;
        }

        [JsonProperty("success")]
        public string Success
        {
            get { return m_Success; }
            set { m_Success = value; }
        }

        private string m_Success;
        [JsonProperty("error-codes")]
        public List<string> ErrorCodes
        {
            get { return m_ErrorCodes; }
            set { m_ErrorCodes = value; }
        }


        private List<string> m_ErrorCodes;
    }
}