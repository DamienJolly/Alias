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

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.NavigatorSettingsMessageComposer);
			message.Int(this.UserPreference.X);
			message.Int(this.UserPreference.Y);
			message.Int(this.UserPreference.Width);
			message.Int(this.UserPreference.Height);
			message.Boolean(this.UserPreference.ShowSearches);
			message.Int(this.UserPreference.UnknownInt);
			return message;
		}
	}
}
