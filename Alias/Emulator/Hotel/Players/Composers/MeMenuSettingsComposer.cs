using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Composers
{
	class MeMenuSettingsComposer : IPacketComposer
	{
		private Player _player;

		public MeMenuSettingsComposer(Player player)
		{
			_player = player;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.MeMenuSettingsMessageComposer);
			message.WriteInteger(_player.Settings.VolumeSystem);
			message.WriteInteger(_player.Settings.VolumeFurni);
			message.WriteInteger(_player.Settings.VolumeTrax);
			message.WriteBoolean(_player.Settings.OldChat);
			message.WriteBoolean(_player.Settings.IgnoreInvites);
			message.WriteBoolean(_player.Settings.CameraFollow);
			message.WriteInteger(1); // friend state?
			message.WriteInteger(0); // chat colour?
			message.WriteInteger(0); // dunno?
			return message;
		}
	}
}
