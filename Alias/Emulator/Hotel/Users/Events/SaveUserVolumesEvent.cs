using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Events
{
	class SaveUserVolumesEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int system = message.PopInt();
			int furni = message.PopInt();
			int trax = message.PopInt();

			session.Habbo.Settings.VolumeSystem = system;
			session.Habbo.Settings.VolumeFurni = furni;
			session.Habbo.Settings.VolumeTrax = trax;
		}
	}
}
