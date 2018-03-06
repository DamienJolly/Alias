namespace Alias.Emulator.Hotel.Moderation
{
    public class ModerationTicket
    {
		public int Id
		{
			get; set;
		} = 0;

		public ModerationTicketState State
		{
			get; set;
		} = ModerationTicketState.OPEN;

		public ModerationTicketType Type
		{
			get; set;
		} = ModerationTicketType.NORMAL;

		public int Category
		{
			get; set;
		} = 1;

		public int Timestamp
		{
			get; set;
		} = 0;

		public int Priority
		{
			get; set;
		} = 0;

		public int ReportedId
		{
			get; set;
		} = 0;

		public string ReportedUsername
		{
			get; set;
		} = "Uknown";

		public int RoomId
		{
			get; set;
		} = 0;

		public int SenderId
		{
			get; set;
		} = 0;

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
