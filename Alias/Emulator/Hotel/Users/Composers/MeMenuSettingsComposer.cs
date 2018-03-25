using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Composers
{
	public class MeMenuSettingsComposer : IPacketComposer
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
			result.Int(1); // friend state?
			result.Int(0); // chat colour?
			result.Int(0); // dunno?
			return result;
		}
	}
}
