using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Messenger.Composers
{
	public class RoomInviteComposer : IPacketComposer
	{
		private int Sender;
		private string Message;

		public RoomInviteComposer(int sender, string message)
		{
			this.Sender = sender;
			this.Message = message;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomInviteMessageComposer);
			message.WriteInteger(this.Sender);
			message.WriteString(this.Message);
			return message;
		}
	}
}
