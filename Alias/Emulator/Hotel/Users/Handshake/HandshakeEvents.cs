using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Hotel.Users.Handshake.Events;

namespace Alias.Emulator.Hotel.Users.Handshake
{
	public class HandshakeEvents
	{
		public static void Register()
		{
			MessageHandler.Register(Incoming.ReleaseVersionMessageEvent, new ReleaseVersionEvent());
			MessageHandler.Register(Incoming.SecureLoginMessageEvent, new SecureLoginEvent());
			MessageHandler.Register(Incoming.MachineIDMessageEvent, new MachineIDEvent());
			MessageHandler.Register(Incoming.RequestUserDataMessageEvent, new RequestUserDataEvent());
			MessageHandler.Register(Incoming.VersionCheckMessageEvent, new VersionCheckEvent());
		}
	}
}
