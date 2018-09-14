using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Misc.Events
{
	class ReleaseVersionEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			string version = message.PopString();

			if (version != Alias.ProductionVersion)
			{
				Logging.Info("User connected with " + version + " instead of " + Alias.ProductionVersion);
				session.Disconnect();
			}
		}
	}
}
