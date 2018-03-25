using Alias.Emulator.Hotel.Users.Composers;
using Alias.Emulator.Hotel.Users.Messenger;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Events
{
    class RequestProfileFriendsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			int userId = message.Integer();
			Habbo habbo = SessionManager.HabboById(userId);

			if (habbo.Messenger != null)
			{
				session.Send(new ProfileFriendsComposer(userId, habbo.Messenger.FriendList()));
			}
			else
			{
				session.Send(new ProfileFriendsComposer(userId, MessengerComponent.GetFriendList(userId)));
			}
		}
	}
}
