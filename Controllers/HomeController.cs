using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using VertoTest.Models;

namespace VertoTest.Controllers
{
    public class HomeController : Controller
    {
        // For logging errors
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Returns the HTML content from Index.cshtml
        public IActionResult Index()
        {
            var model = new Models.Home.Index();

            model.Cards = ListCards();

            return View(model);
        }

        // Returns the HTML content from Edit.cshtml
        public IActionResult Edit()
        {
            var model = new Models.Home.Edit();
            model.Cards = ListCards();
            return View(model);
        }

        // Connection to the Opticron database stored in ./bin/Debug/net8.0
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Opticron.mdf;Integrated Security=True";


        // This returns a list of (model - card.cs) cards
        // The data from this list is used in the cshtml files
        // An ajax call in edit.js calls this
        public List<Card> ListCards()
        {
            // The list of card models to return
            var cards = new List<Card>();

            // Connect to the database
            using (SqlConnection connection = new(connectionString))
            {
                // The query to use
                string query = "SELECT * FROM [dbo].[HTMLCards]";

                // Connection
                using SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Iterate through the results create Card objects
                    while (reader.Read())
                    {
                        Card card = new Card();

                        // Each of these checks for the DBNull value before passing (so no errors)
                        int idOrdinal = reader.GetOrdinal("id");
                        if (!reader.IsDBNull(idOrdinal))
                        {
                            card.Id = reader.GetInt32(idOrdinal);
                        }

                        int titleOrdinal = reader.GetOrdinal("title");
                        if (!reader.IsDBNull(titleOrdinal))
                        {
                            card.Title = reader.GetString(titleOrdinal);
                        }

                        int descriptionOrdinal = reader.GetOrdinal("description");
                        if (!reader.IsDBNull(descriptionOrdinal))
                        {
                            card.Description = reader.GetString(descriptionOrdinal);
                        }

                        int imageOrdinal = reader.GetOrdinal("image");
                        if (!reader.IsDBNull(imageOrdinal))
                        {
                            card.FilePath = reader.GetString(imageOrdinal);
                        }

                        int linkOrdinal = reader.GetOrdinal("link");
                        if (!reader.IsDBNull(linkOrdinal))
                        {
                            card.Link = reader.GetString(linkOrdinal);
                        }

                        int categoryOrdinal = reader.GetOrdinal("category");
                        if (!reader.IsDBNull(categoryOrdinal))
                        {
                            card.Category = reader.GetString(categoryOrdinal);
                        }

                        // The card is built and added to the cards list
                        cards.Add(card);
                    }
                }
            }
            // Card information stored in the databse are now available in the cshtml files
            return cards;
        }

        // This is for the CMS (Edit.cshtml) page
        // This allows for a user to update a specific cards information such as image, description, title, and link
        public ActionResult UpdateCard(int cardId, string title, string description, string file, string link, string category)
        {
            // To check the database is actually updated (error checking)
            int rowsAffected = 0;

            // Connect to database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Query (incomplete as added to)
                string query = "UPDATE [dbo].[HTMLCards] SET ";

                // Create a dictionary for parameters...
                Dictionary<string, object> parameters = new Dictionary<string, object>();

                // ... so parameters added conditionally based on whether they're provided or empty
                if (!string.IsNullOrEmpty(title))
                {
                    query += "title = @title, ";
                    parameters["@title"] = title;
                }
                if (!string.IsNullOrEmpty(description))
                {
                    query += "description = @description, ";
                    parameters["@description"] = description;
                }
                if (!string.IsNullOrEmpty(file))
                {
                    query += "image = @file, ";
                    parameters["@file"] = file;
                }
                if (!string.IsNullOrEmpty(link))
                {
                    query += "link = @link, ";
                    parameters["@link"] = link;
                }
                if (!string.IsNullOrEmpty(category))
                {
                    query += "category = @category, ";
                    parameters["@category"] = category;
                }

                // Removes the comma at the end (so no error)
                if (query.EndsWith(", "))
                {
                    query = query.Remove(query.Length - 2);
                }

                // Add WHERE clause to query (is now complete) so that cardId can be input
                query += " WHERE id = @cardId";

                // Update the database
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Ensure that the correct card (by id) is updated
                    command.Parameters.AddWithValue("@cardId", cardId);

                    // Add the parameters from the dictionary
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.Key, parameter.Value ?? DBNull.Value); // Adds as DBNull if value not entered by user
                    }

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            // Return a response indicating success or failure (will post to console in browser)
            if (rowsAffected > 0)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        // This allows a new card to be created in Edit.cshtml
        public ActionResult NewCard(string newTitle, string newDescription, string newFile, string newLink, string category)
        {
            // To check the database is actually updated (error checking)
            int rowsAffected = 0;

            // Connect to database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Query to add a new card
                string query = "INSERT INTO [dbo].[HTMLCards] (title, image, description, link, category) VALUES (@Title, @Image, @Description, @Link, @Category)";

                // Add the parameters (DBNull if empty) to the VALUES part of the query 
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters["@Title"] = string.IsNullOrEmpty(newTitle) ? DBNull.Value : (object)newTitle;
                parameters["@Image"] = string.IsNullOrEmpty(newFile) ? DBNull.Value : (object)newFile;
                parameters["@Description"] = string.IsNullOrEmpty(newDescription) ? DBNull.Value : (object)newDescription;
                parameters["@Link"] = string.IsNullOrEmpty(newLink) ? DBNull.Value : (object)newLink;
                parameters["@Category"] = string.IsNullOrEmpty(category) ? DBNull.Value : (object)category;

                // Add into the database
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add the parameters (DBNull if DBNull / null)
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.Key, parameter.Value ?? DBNull.Value);
                    }

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            // Return a response indicating success or failure (will post to console in browser)
            if (rowsAffected > 0)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }


        // This allows a user to delete a card
        public ActionResult DeleteCard(int cardId)
        {
            // Connect to database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // SQL Query
                string query = "DELETE FROM HTMLCards WHERE id = @ID";

                // Add the id to be deleted
                var parameters = new Dictionary<string, object>
                {
                    { "@ID", cardId }
                };

                // Execute deletion query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add the id parameter
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    // Error checking
                    if (rowsAffected > 0)
                    {
                        // Deletion successful
                        return Json(new { success = true });
                    }
                    else
                    {
                        // No rows were affected, meaning the row with the provided ID does not exist
                        return Json(new { success = false, message = "No matching record found for deletion." });
                    }
                }
            }
        }

        // Reset the database to default values
        public ActionResult ResetDatabase()
        {
            try
            {
                // Uses SQL from a.sql file stored in the path below
                string sqlFilePath = @"wwwroot/sql/ResetDatabase.sql";
                string sqlQuery = System.IO.File.ReadAllText(sqlFilePath); // Turns it into a string

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        connection.Open(); 
                        command.ExecuteNonQuery(); // Reset the database
                    }
                }

                return Json(new { success = true, message = "Database reset successfully." }); // Sends a message to the console in the browser (erorr checking)
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Failed to reset the database: " + ex.Message });
            }
        }

        // Get a card by ID
        public JsonResult GetCard(int cardId)
        {
            var cards = ListCards();
            var card = cards.Where(x => x.Id == cardId).FirstOrDefault();

            return Json(card); // Returns a card (card.cs model) 
        }

        // Error logging
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
