namespace ChristianBookClub.Web.ViewModels
{
	public class SubscriptionViewModel
	{

		public long SeminarId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Details { get; set; } = string.Empty;
		public bool Registered { get; set; }
		public DateTime NextMeeting { get; set; }
		public DateOnly NextMeetingDate { get { return DateOnly.FromDateTime(NextMeeting); } }
		public TimeOnly NextMeetingTime { get { return TimeOnly.FromDateTime(NextMeeting); } }
	}
}
