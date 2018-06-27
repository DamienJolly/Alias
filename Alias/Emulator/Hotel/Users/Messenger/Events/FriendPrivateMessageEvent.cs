using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Messenger.Events
{
	class FriendPrivateMessageEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (session.Habbo.Muted)
			{
				return;
			}

			session.Habbo.Messenger.Message(message.PopInt(), message.PopString());
		}
	}
}
