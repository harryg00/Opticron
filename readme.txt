Harry Goswell – Virto Competency Test Documentation
Overview:
This project contains 3 main HTML files, 2 main CSS files, and 2 JS files. 
Index.cshtml is the home page, which takes information stored in the database, and represents as HTML
Edit.cshtml is the CMS admin page where information is seeded into the database automatically 
-	Information which the user decides to change is not checked at the moment (first thing I would change about this project). Instead, if the user gets an error, they can reset the database to defaults
-	There is also no availability to upload an image file (instead can only upload a link to an image), there were some issues I had here. This is the second this I would change about this project, however I thought the ability for the user to insert an image link would be suitable instead. 
Site.css defines the styling for the header and footer (which is used in Views -> Shared -> _Layout)
Style.css defines the styling for the body of Index.cshtml. It includes the styling for the sliders, as well as the information displayed (such as cards)
Wwwroot -> sql -> ResetDatabase.sql is an SQL query used to reset the database to default values. Other queries can be added here, some queries are stored as a string in HomeController.cs
The database itself is stored locally in ./VertoTest/bin/Debug/net8.0/Optricon.mdf
See comments on individual files for more specific information relating to its need / function. 

The project can be opened in Visual Studio Community (and ran from in there) using VertoTest.sln