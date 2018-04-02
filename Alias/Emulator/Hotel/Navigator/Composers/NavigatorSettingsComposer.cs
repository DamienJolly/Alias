using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Navigator.Composers
{
	public class NavigatorSettingsComposer : IPacketComposer
	{
		NavigatorPreference UserPreference;

		public NavigatorSettingsComposer(NavigatorPreference userpref)
		{
			this.UserPreference = userpref;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.NavigatorSettingsMessageComposer);
			message.WriteInteger(this.UserPreference.X);
			message.WriteInteger(this.UserPreference.Y);
			message.WriteInteger(this.UserPreference.Width);
			message.WriteInteger(this.UserPreference.Height);
			message.WriteBoolean(this.UserPreference.ShowSearches);
			message.WriteInteger(this.UserPreference.UnknownInt);
			return message;
		}
	}
}
