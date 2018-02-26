using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Composers
{
	public class MeMenuSettingsComposer : MessageComposer
	{
		private Habbo habbo;

		public MeMenuSettingsComposer(Habbo habbo)
		{
			this.habbo = habbo;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.MeMenuSettingsMessageComposer);
			result.Int(this.habbo.Settings.VolumeSystem);
			result.Int(this.habbo.Settings.VolumeFurni);
			result.Int(this.habbo.Settings.VolumeTrax);
			result.Boolean(this.habbo.Settings.OldChat);
			result.Boolean(this.habbo.Settings.IgnoreInvites);
			result.Boolean(this.habbo.Settings.CameraFollow);
			result.Int(1); // dunno?
			result.Int(0); // chat colour
			return result;
		}
	}
}
