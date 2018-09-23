using Alias.Emulator.Hotel.Players.Messenger.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Messenger.Events
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
			session.Send(new FriendsComposer(session.Player.Messenger.Friends.Values));

			//todo: Offline messages
		}
	}
}
