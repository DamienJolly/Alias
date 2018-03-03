namespace Alias.Emulator.Hotel.Moderation
{
    public class ModerationTicket
    {
		public int Id
		{
			get; set;
		}

		public ModerationTicketState State
		{
			get; set;
		}

		public ModerationTicketType Type
		{
			get; set;
		}

		public int Category
		{
			get; set;
		}

		public int Timestamp
		{
			get; set;
		}
		public int Priority
		{
			get; set;
		}
		public int ReportedId
		{
			get; set;
		}
		public string ReportedUsername
		{
			get; set;
		} = "Uknown";

		public int RoomId
		{
			get; set;
		}

		public int SenderId
		{
			get; set;
		}

		public string SenderUsername
		{
			get; set;
		} = "Unknown";

		public int ModId
		{
			get; set;
		} = 0;

		public string ModUsername
		{
			get; set;
		} = "";

		public string Message
		{
			get; set;
		} = "Unknown Message";
	}
}
