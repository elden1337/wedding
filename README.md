# Wedding
My first personal MVC-project was this wedding site. 
As a product owner, I do see a lot of code in my everyday job, but don't get to code much myself.<br>
The aim was to create an MVC-application in ASP.NET to learn basic setups of the trade, plus to provide myself and Lisa with an easy-to-use platform as a wedding RSVP-application with infos, images etc.<br><br>

### Phase 1
In practical terms, this meant setting up an SQL-server database in Azure, where I created a set of guests, and combined them into couples and families by adding a token, which was then sent to them on the back of their invitation.<br>
Upon login in to the site via one of the external providers used (Facebook, Microsoft, Google) or by creating a local account, they were prompted with an input where the token was added, which then gave them access to RSVP to their whole family.<br>
The RSVP itself was editable, so the token was stored together with the userid in the ASPNetUsers-table.<br><br>

### Phase 2
Once the project was up and running, and the RSVP's started flowing in, I began creating an image-uploading service. To do this, I created a blog-storage on Azure, and an upload-page in my application. However, the images uploaded were too large to show on the web, so to retain the original-size, as well as providing fast-loading copies, I created an Azure-webjob and a queue which resized the images. I then added the resized-uri to the database and showed that in the views.
<br><br>
Part of the first upload to blob:<br>
```c#
 
 for (int i = 0; i < files.Count(); i++)
 {
     var file = files[i];

     var exifcreateddatetime = new DateTime(2020, 3, 24, 8, 43, 00);
     String fileextention;
     fileextention = Path.GetExtension(file.FileName).ToLower();

     String uploadtime = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss.fff");

     CloudBlockBlob blockBlob = container.GetBlockBlobReference(uploadtime + fileextention);

     blockBlob.UploadFromStream(file.InputStream);

```
<br>
In order to read the EXIF-data from the uploaded images (for sorting on date-taken), I had to play around quite a bit. I solved it like this:<br>
```c#

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

```

A quick schema of the upload and image-resize through webjob *(webjob not present in this repo)*<br>
![webjob schema](/Readmefiles/azure_webjob_schema.jpg)<br><br>

I then uploaded parameters to the media-table in the database and showed it on an admin-page for approval, before exposing the images to all the guests.<br>
<hr>

<br>
What I learnt and experimented with during the project:
* GoogleMaps-styling (see Views/Start/Info)
* MDL-styling framework utilized throughout
* Responsive design with viewports Tablet, Phone and Desktop in mind
* Entity Framework
* SQL-database setup
* C#
* ASP.NET
* Some Jquery
* Azure webjobs *(not in this repo)*
* Azure blobstorage and queue *(not in this repo)*

What I experimented with somewhat but didn't finalize:
* Google Matrix API, to be able to create a commuter-service for the guests
* A toast-master chat which was supposed to be hashed to not be visible in database (to me, the groom). See the Toast-section of the repo.
* Various small API's that I was too unskilled to implement


## Screenshots
<br><br>
![Desktop Frontpage](/Readmefiles/frontpage.JPG)
The startpage on desktop.<br>

![Mobile Frontpage w menu open](/Readmefiles/mobilestart.JPG)<br>
The startpage on mobile portrait, with MDL-mobile menu opened.<br>

![Part of the guest-form](/Readmefiles/guest.JPG)<br>
Part of the guest-form where they filled out their contact info, food prefs and lodging prefs (swedish).<br>

![Desktop Image-page w uploadform opened](/Readmefiles/bilder.JPG)
The image-page on desktop, with image-uploadform opened.<br>
<br><br>

## References (some of them):
* https://getmdl.io/
* https://azure.microsoft.com/sv-se/documentation/articles/websites-dotnet-webjobs-sdk-get-started/
* http://stackoverflow.com/questions/36869097/azure-webjobs-blob-trigger-multiple-resizes
