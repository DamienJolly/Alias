using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Events
{
	class SaveUserVolumesEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int system = message.PopInt();
			int furni = message.PopInt();
			int trax = message.PopInt();

			session.Player.Settings.VolumeSystem = system;
			session.Player.Settings.VolumeFurni = furni;
			session.Player.Settings.VolumeTrax = trax;
		}
	}
}
