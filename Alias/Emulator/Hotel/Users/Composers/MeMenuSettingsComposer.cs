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

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.MeMenuSettingsMessageComposer);
			message.WriteInteger(this.habbo.Settings.VolumeSystem);
			message.WriteInteger(this.habbo.Settings.VolumeFurni);
			message.WriteInteger(this.habbo.Settings.VolumeTrax);
			message.WriteBoolean(this.habbo.Settings.OldChat);
			message.WriteBoolean(this.habbo.Settings.IgnoreInvites);
			message.WriteBoolean(this.habbo.Settings.CameraFollow);
			message.WriteInteger(1); // friend state?
			message.WriteInteger(0); // chat colour?
			message.WriteInteger(0); // dunno?
			return message;
		}
	}
}
