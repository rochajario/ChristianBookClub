namespace ChristianBookClub.Domain.Models
{
	public class EmailMessageModel
	{
		public string ToAddress { get; set; } = string.Empty;
		public string Subject { get; set; } = string.Empty;
		public string Body { get; set; } = string.Empty;
	}
}
