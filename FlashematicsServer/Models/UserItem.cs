namespace FlashematicsServer.Models
{
    public class UserItem
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public DateTime RegisterAt { get; set; }
    }
}
