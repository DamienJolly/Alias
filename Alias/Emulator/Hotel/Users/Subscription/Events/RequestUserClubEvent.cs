using Alias.Emulator.Hotel.Users.Subscription.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Subscription.Events
{
    class RequestUserClubEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			//todo: subscription
			session.Send(new UserClubComposer());
		}
	}
}
