namespace VertoTest.Models
{
    // This is the model for the card object, which is used by the webpage to load information from 
    // the database into the HTML
    // This object stores information such as the filepath (image) and link. 
    public class Card
    {
        public int? Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? FilePath { get; set; }

        public string? Link { get; set; }
        public string? Category { get; set; }
    }
}
