namespace ChristianBookClub.Domain.Models.Forms
{
    public class ScheduleForm
    {
        public DateTime MeetingDate { get; set; }
        public string MeetingDetails { get; set; } = string.Empty;
    }
}
