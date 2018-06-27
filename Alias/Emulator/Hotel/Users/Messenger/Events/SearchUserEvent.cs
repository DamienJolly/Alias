using Alias.Emulator.Hotel.Users.Messenger.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Messenger.Events
{
	class SearchUserEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			string username = message.PopString().Replace(" ", "");

			if (string.IsNullOrEmpty(username))
			{
				return;
			}

			session.Send(new UserSearchResultComposer(MessengerDatabase.Search(username), session.Habbo.Messenger));
		}
	}
}
