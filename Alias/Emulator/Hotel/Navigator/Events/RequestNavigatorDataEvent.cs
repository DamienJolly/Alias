using Alias.Emulator.Hotel.Navigator.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Events
{
	public class RequestNavigatorDataEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			session.Send(new NavigatorMetaDataComposer());
			session.Send(new NavigatorLiftedRoomsComposer());
			session.Send(new NavigatorCollapsedCategoriesComposer());
			session.Send(new NavigatorSavedSearchesComposer(session.Habbo.NavigatorPreference.NavigatorSearches));
			session.Send(new NavigatorEventCategoriesComposer(session.Habbo.Rank));
		}
	}
}
