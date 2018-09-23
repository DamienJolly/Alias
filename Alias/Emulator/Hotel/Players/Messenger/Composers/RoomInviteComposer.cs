using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Messenger.Composers
{
	public class RoomInviteComposer : IPacketComposer
	{
		private readonly int _sender;
		private readonly string _message;

		public RoomInviteComposer(int sender, string message)
		{
			_sender = sender;
			_message = message;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomInviteMessageComposer);
			message.WriteInteger(_sender);
			message.WriteString(_message);
			return message;
		}
	}
}
