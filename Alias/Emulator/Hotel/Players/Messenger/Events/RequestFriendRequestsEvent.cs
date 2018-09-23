using Alias.Emulator.Hotel.Players.Messenger.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Messenger.Events
{
	class RequestFriendRequestsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			session.Send(new LoadFriendRequestsComposer(session.Player.Messenger.Requests.Values));
		}
	}
}
