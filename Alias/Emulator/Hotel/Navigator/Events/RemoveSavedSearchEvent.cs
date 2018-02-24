using Alias.Emulator.Hotel.Navigator.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Events
{
	public class RemoveSavedSearchEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			int id = message.Integer();

			session.Habbo().NavigatorPreference.RemoveSearch(id);

			session.Send(new NavigatorSavedSearchesComposer(session.Habbo().NavigatorPreference.NavigatorSearches));
		}
	}
}
