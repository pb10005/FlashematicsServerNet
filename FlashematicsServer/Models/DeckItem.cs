namespace FlashematicsServer.Models
{
    public class DeckItem
    {
        public long Id { get; set; }
        public string Name { get; set; }    
        public string Base64 { get; set; }  

        public DateTime UpdatedAt { get; set; }
    }
}
