namespace ChristianBookClub.Data.Entities
{
	public class UserHistoric
	{
		public long SeminarId { get; set; }
		public long UserId { get; set; }
		public string Name { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
		public string SureName { get; set; } = string.Empty;
		public int TotalMeetings { get; set; }
		public int FinishedMeetings { get; set; }
		public float PresenceRate { get; set; }
	}
}
