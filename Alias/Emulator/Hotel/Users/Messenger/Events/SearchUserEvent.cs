using Alias.Emulator.Hotel.Users.Messenger.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Messenger.Events
{
	public class SearchUserEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			string username = message.String().Replace(" ", "");

			if (string.IsNullOrEmpty(username))
			{
				return;
			}

			session.Send(new UserSearchResultComposer(MessengerDatabase.Search(username), session.Habbo().Messenger()));
		}
	}
}
