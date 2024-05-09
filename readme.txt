Harry Goswell – Virto Competency Test Documentation
Overview:
This project contains 3 main HTML files, 2 main CSS files, and 2 JS files. 
Index.cshtml is the home page, which takes information stored in the database, and represents as HTML
Edit.cshtml is the CMS admin page where information is seeded into the database automatically 
-	Information which the user decides to change is not checked at the moment (first thing I would change about this project). Instead, if the user gets an error, they can reset the database to defaults
-	There is also no availability to upload an image file (instead can only upload a link to an image), there were some issues I had here. This is the second thing I would change about this project, however I thought the ability for the user to insert an image link instead would be suitable. 
Site.css defines the styling for the header and footer (HTML in Views -> Shared -> _Layout.cshtml)
Style.css defines the styling for the body of Index.cshtml. It includes the styling for the sliders, as well as the information displayed (such as cards)
wwwroot -> sql -> ResetDatabase.sql is an SQL query used to reset the database to default values. Other queries can be added here, some queries are stored as a string in HomeController.cs
The database itself is stored locally in VertoTest/bin/Debug/net8.0/Optricon.mdf
- In the event the database does not get pulled from this repo (thus causing an error), download the two files from the zip folder (OpticronDB.zip) and place them in VertoTest/bin/Debug/net8.0
See comments on individual files for more specific information relating to its need / function. 

The project can be opened in Visual Studio Community (and ran from in there) using VertoTest.sln
