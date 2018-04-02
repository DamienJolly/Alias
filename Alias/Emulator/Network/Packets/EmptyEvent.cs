using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Network.Packets
{
	public class EmptyEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Logging.Debug("Unregistered Event with Id " + message.Header + " handled by the Placeholder event.");
		}
	}
}
