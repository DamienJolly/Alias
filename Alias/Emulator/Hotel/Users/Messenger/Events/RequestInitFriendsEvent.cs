using Alias.Emulator.Hotel.Users.Messenger.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Messenger.Events
{
	class RequestInitFriendsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (!int.TryParse(Alias.Server.Settings.GetSetting("maximum.friends"), out int maxFriends))
			{
				maxFriends = 1000;
			}

			session.Send(new MessengerInitComposer(maxFriends));
			session.Send(new FriendsComposer(session.Habbo.Messenger.FriendList()));

			//todo: Offline messages
		}
	}
}
