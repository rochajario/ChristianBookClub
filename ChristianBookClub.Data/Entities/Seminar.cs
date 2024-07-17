using System.ComponentModel.DataAnnotations;

namespace ChristianBookClub.Data.Entities
{
    public class Seminar : BaseEntity<Seminar>
    {

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Text)]
        public string CoverImage { get; set; } = string.Empty;

        public ICollection<SeminarSchedule>? SeminarSchedules { get; set; }

        public override void SetValue(Seminar entity)
        {
            Name = entity.Name;
            Description = entity.Description;
            CoverImage = entity.CoverImage;
            SeminarSchedules = entity.SeminarSchedules;
        }
    }
}
