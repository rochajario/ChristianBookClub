using System.ComponentModel.DataAnnotations;

namespace ChristianBookClub.Data.Entities
{
	public class PublicUpcomingSeminar
	{
		public long SeminarId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string CoverImage { get; set; } = string.Empty;
		public DateTime NextMeeting { get; set; }
		public string Details { get; set; } = string.Empty;
	}
}
