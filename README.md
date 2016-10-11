# Wedding
My first personal MVC-project was this wedding site. 
As a product owner, I do see a lot of code in my everyday job, but don't get to code much myself.<br>

The aim was to create an MVC-application in ASP.NET to learn basic setups of the trade. The purpose of creating it was to provide myself and Lisa with an easy-to-use platform as a wedding RSVP-application.<br><br>
In practical terms, this meant setting up an SQL-server database in Azure, where I created a set of guests, and combined them into couples and families by adding a token, which was then sent to them on the back of their invitation.<br>
Upon login in to the site via one of the external providers used (Facebook, Microsoft, Google) or by creating a local account, they were prompted with an input where the token was added, which then gave them access to RSVP to their whole family.<br>
The RSVP itself was editable, so the token was stored together with the 

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
* Various small API's that I was too unskilled to implement
* A toast-master chat which was supposed to be hashed to not be visible in database (to me, the groom). See the Toast-section of the repo.
* A 


## Screenshots
<br><br>
![Desktop Frontpage](/Readmefiles/frontpage.JPG)
The startpage on desktop.<br>

![Mobile Frontpage w menu open](/Readmefiles/mobilestart.JPG)
The startpage on mobile portrait, with MDL-mobile menu opened.<br>

![Part of the guest-form](/Readmefiles/guest.JPG)
Part of the guest-form where they filled out their contact info, food prefs and lodging prefs (swedish).<br>

![Desktop Image-page w uploadform opened](/Readmefiles/bilder.JPG)
The image-page on desktop, with image-uploadform opened.<br>


## References (some of them):
* https://getmdl.io/
* https://azure.microsoft.com/sv-se/documentation/articles/websites-dotnet-webjobs-sdk-get-started/
* http://stackoverflow.com/questions/36869097/azure-webjobs-blob-trigger-multiple-resizes
