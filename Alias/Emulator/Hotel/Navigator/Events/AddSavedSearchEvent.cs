using Alias.Emulator.Hotel.Navigator.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Events
{
	public class AddSavedSearchEvent : IPacketEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			string page = message.String();
			string searchCode = message.String();

			if (session.Habbo.NavigatorPreference.HasPage(page, searchCode))
				return;

			session.Habbo.NavigatorPreference.AddSearch(page, searchCode);

			session.Send(new NavigatorSavedSearchesComposer(session.Habbo.NavigatorPreference.NavigatorSearches));
		}
	}
}
