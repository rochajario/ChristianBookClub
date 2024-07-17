using System.ComponentModel.DataAnnotations;

namespace ChristianBookClub.Data.Entities
{
    public class SeminarRegister : BaseEntity<SeminarRegister>
    {
        [Required]
        public long SeminarId { get; set; }

        [Required]
        public long UserId { get; set; }

        public Seminar? Seminar { get; set; }
        public User? User { get; set; }

        public override void SetValue(SeminarRegister entity)
        {
            SeminarId = entity.SeminarId;
            UserId = entity.UserId;
            Seminar = entity.Seminar;
            User = entity.User;
        }
    }
}
