using Alias.Emulator.Hotel.Users.Messenger.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Messenger.Events
{
	public class RequestInitFriendsEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			session.Send(new MessengerInitComposer());
			session.Send(new FriendsComposer(session.Habbo.Messenger.FriendList()));

			//todo: Offline messages
		}
	}
}
