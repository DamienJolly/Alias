using Alias.Emulator.Hotel.Rooms.Entities.Chat;

namespace Alias.Emulator.Hotel.Moderation
{
    public class ModerationChatlog
    {
		public int UserId
		{
			get; set;
		}

		public string Username
		{
			get; set;
		}

		public int TargetId
		{
			get; set;
		}

		public string TargetUsername
		{
			get; set;
		}

		public int Timestamp
		{
			get; set;
		}

		public string Message
		{
			get; set;
		}

		public ChatType Type
		{
			get; set;
		}
    }
}
