using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Events
{
	public class SaveWindowSettingsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			session.Habbo.NavigatorPreference.X = message.PopInt();
			session.Habbo.NavigatorPreference.Y = message.PopInt();
			session.Habbo.NavigatorPreference.Width = message.PopInt();
			session.Habbo.NavigatorPreference.Height = message.PopInt();
			session.Habbo.NavigatorPreference.ShowSearches = message.PopBoolean();
			session.Habbo.NavigatorPreference.UnknownInt = message.PopInt();
			NavigatorDatabase.SavePreferences(session.Habbo.NavigatorPreference, session.Habbo.Id);
		}
	}
}
