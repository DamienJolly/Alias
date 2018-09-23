using Alias.Emulator.Hotel.Players.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Events
{
    class RequestProfileFriendsEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			int userId = message.PopInt();
			Player player = await Alias.Server.PlayerManager.ReadPlayerByIdAsync(userId);

			if (player.Messenger != null)
			{
				session.Send(new ProfileFriendsComposer(userId, player.Messenger.Friends.Values));
			}
			else
			{
				//todo:
				//session.Send(new ProfileFriendsComposer(userId, MessengerComponent.GetFriendList(userId)));
			}
		}
	}
}
