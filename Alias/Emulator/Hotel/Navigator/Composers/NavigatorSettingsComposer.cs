using Alias.Emulator.Hotel.Players.Navigator;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Navigator.Composers
{
	class NavigatorSettingsComposer : IPacketComposer
	{
		NavigatorSettings _settings;

		public NavigatorSettingsComposer(NavigatorSettings settings)
		{
			_settings = settings;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.NavigatorSettingsMessageComposer);
			message.WriteInteger(_settings.X);
			message.WriteInteger(_settings.Y);
			message.WriteInteger(_settings.Width);
			message.WriteInteger(_settings.Height);
			message.WriteBoolean(_settings.ShowSearches);
			message.WriteInteger(_settings.UnknownInt);
			return message;
		}
	}
}
