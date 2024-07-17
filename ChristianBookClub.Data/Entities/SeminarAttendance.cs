using System.ComponentModel.DataAnnotations;

namespace ChristianBookClub.Data.Entities
{
    public class SeminarAttendance : BaseEntity<SeminarAttendance>
    {
        [Required]
        public long SeminarScheduleId { get; set; }

        [Required]
        public long SeminarRegisterId { get; set; }

        public SeminarSchedule? SeminarSchedule { get; set; }
        public SeminarRegister? SeminarRegister { get; set; }

        public override void SetValue(SeminarAttendance entity)
        {
            SeminarScheduleId = entity.SeminarScheduleId;
            SeminarRegisterId = entity.SeminarRegisterId;
            SeminarSchedule = entity.SeminarSchedule;
            SeminarRegister = entity.SeminarRegister;
        }
    }
}
