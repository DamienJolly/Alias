using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Users.Composers
{
	public class RoomUserChatComposer : MessageComposer
	{
		private int VirtualId;
		private string Message;
		private int FaceExpression;
		private int MessageColour;
		private ChatType Type;

		public RoomUserChatComposer(int virtualId, string msg, int face, int colour, ChatType type)
		{
			this.VirtualId = virtualId;
			this.Message = msg;
			this.FaceExpression = face;
			this.MessageColour = colour;
			this.Type = type;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(this.Id);
			result.Int(this.VirtualId);
			result.String(this.Message);
			result.Int(this.FaceExpression);
			result.Int(this.MessageColour);
			result.Int(0);
			result.Int(-1);
			return result;
		}

		private uint Id
		{
			get
			{
				switch (this.Type)
				{
					case ChatType.CHAT:
						return Outgoing.RoomUserTalkMessageComposer;
					case ChatType.SHOUT:
						return Outgoing.RoomUserShoutMessageComposer;
					case ChatType.WHISPER:
						return Outgoing.RoomUserWhisperMessageComposer;
					default:
						return Outgoing.RoomUserTalkMessageComposer;
				}
			}
		}
	}
}
