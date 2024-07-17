namespace ChristianBookClub.Data.Entities
{
	public class RegisteredUpcomingSeminar
	{
		public long SeminarId { get; set; }
		public string Name { get; set; } = string.Empty;
		public long UserId { get; set; }
		public string Description { get; set; } = string.Empty;
		public string CoverImage { get; set; } = string.Empty;
		public DateTime NextMeeting { get; set; }
		public string Details { get; set; } = string.Empty;

		public long ScheduleId { get; set; }
		public long RegisterId { get; set; }
		public string RoomId { get; set; } = string.Empty;
	}
}
