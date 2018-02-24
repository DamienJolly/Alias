using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Events
{
	public class SaveWindowSettingsEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			session.Habbo().NavigatorPreference.X = message.Integer();
			session.Habbo().NavigatorPreference.Y = message.Integer();
			session.Habbo().NavigatorPreference.Width = message.Integer();
			session.Habbo().NavigatorPreference.Height = message.Integer();
			session.Habbo().NavigatorPreference.ShowSearches = message.Boolean();
			session.Habbo().NavigatorPreference.UnknownInt = message.Integer();
			NavigatorDatabase.SavePreferences(session.Habbo().NavigatorPreference, session.Habbo().Id);
		}
	}
}
