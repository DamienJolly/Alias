using Alias.Emulator.Hotel.Navigator.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Events
{
	public class SearchRoomsEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			session.Send(new NavigatorSearchResultsComposer(message.String(), message.String(), session));
		}
	}
}
