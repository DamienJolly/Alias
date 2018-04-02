using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Messenger.Events
{
	public class DeclineFriendRequestEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (message.PopBoolean())
			{
				session.Habbo.Messenger.DeclineAll();
			}
			else
			{
				message.PopInt();
				session.Habbo.Messenger.Decline(message.PopInt());
			}
		}
	}
}
