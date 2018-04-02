using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Hotel.Users.Handshake.Events;

namespace Alias.Emulator.Hotel.Users.Handshake
{
	public class HandshakeEvents
	{
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.ReleaseVersionMessageEvent, new ReleaseVersionEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.SecureLoginMessageEvent, new SecureLoginEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.MachineIDMessageEvent, new MachineIDEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestUserDataMessageEvent, new RequestUserDataEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.VersionCheckMessageEvent, new VersionCheckEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.PingMessageEvent, new PingEvent());
		}
	}
}
