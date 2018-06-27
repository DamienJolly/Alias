using Alias.Emulator.Hotel.Users.Messenger.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Messenger.Events
{
	class RequestFriendRequestsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			session.Send(new LoadFriendRequestsComposer(session.Habbo.Messenger));
		}
	}
}
