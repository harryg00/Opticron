namespace VertoTest.Models.Home
{
    public class Index
    {
        public List<Card>? Cards {  get; set; }
        // Links the card model to the Index page (in HomeController)
    }

    public class Edit
    {
        public List<Card>? Cards { get; set; }
        // Links the card model to the Edit page (in HomeController)
    }
}
