using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.IO;
using System.Globalization;
using System.Drawing;

namespace Wedding.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Wedding.Data;
    using Wedding.Models.Social;

    public class SocialController : Controller
    {

        //int? page är PAGINERINGSGREJER
        public ActionResult Index(int? page, bool success = false, bool showmessage = false)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LogIn", "Account");
            }

            //PAGINERINGSGREJER

            int currentPageIndex = page.HasValue ? page.Value : 1;
            int pageSize = 50;
            int startIndex = page.HasValue ? (page.Value - 1) * pageSize : 0;
            int pages = 0;
            int totalCount = 0;
            
            //PAGINERINGSGREJER

            var model = new SocialIndexModel();

            if (showmessage)
            {
                if (success)
                {
                    model = new SocialIndexModel() { SuccessMessage = "Ok" };
                }
                else {
                    model = new SocialIndexModel() { ErrorMessage = "FAILED" };
                }
            }

            using (var db = new Wedding.Data.boda_liveEntities())
            {
                var mediaQuery = from m in db.media
                                 where m.approved.Value == true && m.deleted.Value == false
                                 orderby m.exif_created
                                 select new { m.filetype, m.media_uri };
                model.Blobs.AddRange(
                    mediaQuery.ToList()
                        .Select(
                            x =>
                            new BlobListing()
                            {
                                FileType = x.filetype,
                                ImageUrl = x.media_uri
                            }).Skip(startIndex).Take(pageSize));
                //PAGINERINGSGREJER
                totalCount = mediaQuery.Count();
            }

            //PAGINERINGSGREJER
            pages = (totalCount + pageSize - 1) / pageSize;
            
            model.Currentpage = currentPageIndex;
            model.Pages = pages;

            return this.View(model);
        }


        [HttpPost, ValidateAntiForgeryToken, AsyncTimeout(2400000)]
        public ActionResult PostImage(HttpPostedFileBase[] files)

        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("photos");

            string EncodedResponse = Request.Form["g-Recaptcha-Response"];
            bool IsCaptchaValid = (ReCaptchaClass.Validate(EncodedResponse) == "True" ? true : false);

            if (IsCaptchaValid)
            {


                if (files[0] != null)
                {

                    for (int i = 0; i < files.Count(); i++)
                    {
                        var file = files[i];

                        var exifcreateddatetime = new DateTime(2020, 3, 24, 8, 43, 00);
                        String fileextention;
                        fileextention = Path.GetExtension(file.FileName).ToLower();

                        String uploadtime = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss.fff");

                        CloudBlockBlob blockBlob = container.GetBlockBlobReference(uploadtime + fileextention);

                        blockBlob.UploadFromStream(file.InputStream);
                        //END FIRST BLOB-UPLOAD

                        //START CRAZY EXIFGETDATE
                        file.InputStream.Position = 0;

                        byte[] imageData = new byte[file.ContentLength];

                        file.InputStream.Read(imageData, 0, file.ContentLength);

                        using (Stream inputStream = file.InputStream)
                        {
                            MemoryStream ms = inputStream as MemoryStream;
                            if (ms == null)
                            {
                                ms = new MemoryStream(imageData);
                                StreamReader sr = new StreamReader(ms);

                                inputStream.CopyTo(ms);
                            }

                            Image originalImage = Image.FromStream(ms);

                            exifcreateddatetime = new DateTime(2020, 3, 24, 8, 43, 00);

                            int DateTakenValue = 0x9003; //36867;
                            CultureInfo provider = CultureInfo.InvariantCulture;

                            if (!originalImage.PropertyIdList.Contains(DateTakenValue))
                            {
                                exifcreateddatetime = new DateTime(2020, 3, 24, 8, 43, 00);
                            }
                            else
                            {
                                string dateTakenTag = System.Text.Encoding.ASCII.GetString(originalImage.GetPropertyItem(DateTakenValue).Value);
                                string[] parts = dateTakenTag.Split(':', ' ');
                                int year = int.Parse(parts[0]);
                                int month = int.Parse(parts[1]);
                                int day = int.Parse(parts[2]);
                                int hour = int.Parse(parts[3]);
                                int minute = int.Parse(parts[4]);
                                int second = int.Parse(parts[5]);

                                exifcreateddatetime = new DateTime(year, month, day, hour, minute, second);
                            }
                        }

                        //END CRAZY EXIFGETDATE

                        //START DATABASE UPLOAD
                        using (var db = new Wedding.Data.boda_liveEntities())
                        {

                            media mediaInfo = new media();
                            db.media.Add(mediaInfo);
                            mediaInfo.approved = false;
                            mediaInfo.deleted = false;
                            mediaInfo.exif_created = exifcreateddatetime;
                            mediaInfo.media_owner = User.Identity.Name;
                            mediaInfo.filetype = fileextention;
                            mediaInfo.media_uri = "https://magnuselden.blob.core.windows.net/resizedphotos/" + uploadtime + fileextention;
                            mediaInfo.uploaded = DateTime.Now;

                            db.SaveChanges();
                        }
                        //END DATABASE UPLOAD
                    }
            
                    return RedirectToAction("Index", new { success = true, showmessage = true });
            
                }
            }
            
            return RedirectToAction("Index", new { success = false, showmessage = true });

        }


        //GET ADMIN
        public ActionResult Admin()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LogIn", "Account");
            }

            var model = new SocialIndexModel();

            using (var db = new Wedding.Data.boda_liveEntities())
            {
                var mediaQuery = from m in db.media
                                 where m.deleted.Value == false && m.approved.Value == false
                                 orderby m.exif_created descending
                                 select new { m.filetype, m.media_uri, m.approved, m.deleted, m.media_owner, m.id };
                model.Blobs.AddRange(
                    mediaQuery.ToList()
                        .Select(
                            x =>
                            new BlobListing()
                            {
                                FileType = x.filetype,
                                ImageUrl = x.media_uri,
                                MediaOwner = x.media_owner,
                                Deleted = x.deleted ?? false,
                                Approved = x.approved ?? false,
                                Id = x.id

                            }));
            }

            return this.View(model);
        }



        [HttpPost]
        public ActionResult Admin(SocialIndexModel model)
        {
            if (model == null)
            {
                return this.HttpNotFound("Failed to build model");
            }

            using (var db = new Wedding.Data.boda_liveEntities())
            {
                //foreach (var gg in model.Blobs new List<BlobListing>()))
                for (int i = 0; i < model.Blobs.Count(); i++)
                {
                    var gg = model.Blobs[i];


                    var images = db.media.FirstOrDefault(x => x.id == gg.Id);
                    if (images == null)
                    {
                        throw new Exception("No Media found");
                    }

                    images.approved = gg.Approved;
                    images.deleted = gg.Deleted;

                }

                db.SaveChanges();
            }

            return RedirectToAction("Admin");
        }


        public ActionResult Edit()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LogIn", "Account");
            }

            var model = new SocialIndexModel();

            using (var db = new Wedding.Data.boda_liveEntities())
            {
                var mediaQuery = from m in db.media
                                 where m.deleted.Value == false && m.approved.Value == true
                                 orderby m.exif_created ascending
                                 select new { m.filetype, m.media_uri, m.approved, m.id, m.exif_created };
                model.Blobs.AddRange(
                    mediaQuery.ToList()
                        .Select(
                            x =>
                            new BlobListing()
                            {
                                FileType = x.filetype,
                                ImageUrl = x.media_uri,
                                Approved = x.approved ?? false,
                                Id = x.id,
                                ExifCreated = x.exif_created

                            }));
            }

            return this.View(model);
        }



        [HttpPost]
        public ActionResult Edit(SocialIndexModel model)
        {
            if (model == null)
            {
                return this.HttpNotFound("Failed to build model");
            }

            using (var db = new Wedding.Data.boda_liveEntities())
            {
                //foreach (var gg in model.Blobs new List<BlobListing>()))
                for (int i = 0; i < model.Blobs.Count(); i++)
                {
                    var gg = model.Blobs[i];


                    var images = db.media.FirstOrDefault(x => x.id == gg.Id);
                    if (images == null)
                    {
                        throw new Exception("No Media found");
                    }

                    images.approved = gg.Approved;
                    images.exif_created = gg.ExifCreated;

                }

                db.SaveChanges();
            }

            return RedirectToAction("Edit");
        }

    }
}
