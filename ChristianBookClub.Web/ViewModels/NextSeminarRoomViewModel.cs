using ChristianBookClub.Data.Entities;
using System.Globalization;

namespace ChristianBookClub.Web.ViewModels
{
	public class NextSeminarRoomViewModel(RegisteredUpcomingSeminar seminar)
	{
		public long ScheduleId { get; private set; } = seminar.ScheduleId;
        public string Name { get; private set; } = seminar.Name;
		public string Description { get; private set; } = seminar.Description;
		public string CoverImage { get; private set; } = seminar.CoverImage;
		public string NextMeeting { get; private set; } = seminar.NextMeeting.ToString("o", CultureInfo.InvariantCulture);
		public string MeetingDetails { get; private set; } = seminar.Details;
    }
}
