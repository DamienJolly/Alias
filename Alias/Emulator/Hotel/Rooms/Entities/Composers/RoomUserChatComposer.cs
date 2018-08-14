using Alias.Emulator.Hotel.Rooms.Entities.Chat;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Entities.Composers
{
	public class RoomUserChatComposer : IPacketComposer
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

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(this.Id);
			message.WriteInteger(this.VirtualId);
			message.WriteString(this.Message);
			message.WriteInteger(this.FaceExpression);
			message.WriteInteger(this.MessageColour);
			message.WriteInteger(0);
			message.WriteInteger(-1);
			return message;
		}

		private short Id
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
