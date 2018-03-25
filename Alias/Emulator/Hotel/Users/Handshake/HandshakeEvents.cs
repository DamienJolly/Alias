using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Hotel.Users.Handshake.Events;

namespace Alias.Emulator.Hotel.Users.Handshake
{
	public class HandshakeEvents
	{
		public static void Register()
		{
			Alias.GetServer().GetPacketManager().Register(Incoming.ReleaseVersionMessageEvent, new ReleaseVersionEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.SecureLoginMessageEvent, new SecureLoginEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.MachineIDMessageEvent, new MachineIDEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestUserDataMessageEvent, new RequestUserDataEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.VersionCheckMessageEvent, new VersionCheckEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.PingMessageEvent, new PingEvent());
		}
	}
}
