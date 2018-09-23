using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Events
{
	class SaveWindowSettingsEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			session.Player.Navigator.Settings.X = message.PopInt();
			session.Player.Navigator.Settings.Y = message.PopInt();
			session.Player.Navigator.Settings.Width = message.PopInt();
			session.Player.Navigator.Settings.Height = message.PopInt();
			session.Player.Navigator.Settings.ShowSearches = message.PopBoolean();
			session.Player.Navigator.Settings.UnknownInt = message.PopInt();
			await session.Player.Navigator.UpdateSettings();
		}
	}
}
