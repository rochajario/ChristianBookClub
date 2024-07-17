using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChristianBookClub.Data.Entities
{
    public class SeminarSchedule : BaseEntity<SeminarSchedule>
    {
        [Required]
        public long SeminarId { get; set; }

        [Required]
        public DateTime MeetingDate { get; set; }

        [Required]
        public string MeetingDetails { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RoomId { get; set; } = Guid.NewGuid();

        public Seminar? Seminar { get; set; }

        public override void SetValue(SeminarSchedule entity)
        {
            SeminarId = entity.SeminarId;
            MeetingDate = entity.MeetingDate;
            MeetingDetails = entity.MeetingDetails;
            Seminar = entity.Seminar;
        }
    }
}
