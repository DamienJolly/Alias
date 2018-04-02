using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Messenger.Composers
{
	public class LoadFriendRequestsComposer : IPacketComposer
	{
		private MessengerComponent messenger;

		public LoadFriendRequestsComposer(MessengerComponent messenger)
		{
			this.messenger = messenger;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.LoadFriendRequestsMessageComposer);
			message.WriteInteger(this.messenger.RequestList().Count);
			message.WriteInteger(this.messenger.RequestList().Count);
			this.messenger.RequestList().ForEach(req =>
			{
				message.WriteInteger(req.Id);
				message.WriteString(req.Username);
				message.WriteString(req.Look);
			});
			return message;
		}
	}
}
