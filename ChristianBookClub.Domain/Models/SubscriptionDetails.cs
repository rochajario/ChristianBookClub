namespace ChristianBookClub.Domain.Models
{
	public class SubscriptionDetails
	{
		public long SeminarId { get; set; }
		public string Name { get; set; } = string.Empty;
		public bool Registered { get; set; }
	}
}
