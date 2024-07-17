namespace ChristianBookClub.Domain.Models.Forms
{
    public class SeminarForm
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CoverImage { get; set; } = string.Empty;
        public IEnumerable<ScheduleForm> Schedules { get; set; } = Enumerable.Empty<ScheduleForm>();
    }
}
