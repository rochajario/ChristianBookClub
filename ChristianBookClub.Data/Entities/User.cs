using Microsoft.AspNetCore.Identity;

namespace ChristianBookClub.Data.Entities
{
    public class User : IdentityUser<long>
    {
        public string Firstname { get; set; } = string.Empty;
        public string Surename { get; set; } = string.Empty;
        public string TelegramId { get; set; } = string.Empty;
    }
}
