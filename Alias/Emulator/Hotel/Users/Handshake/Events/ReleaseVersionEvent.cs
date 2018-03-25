using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Users.Handshake.Events
{
	public class ReleaseVersionEvent : IPacketEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			string version = message.String();

			if (version != Constant.ProductionVersion)
			{
				Logging.Info("User connected with " + version + " instead of " + Constant.ProductionVersion);
				session.Disconnect();
			}
		}
	}
}
