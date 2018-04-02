using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Misc.Composers
{
	public class GenericAlertComposer : IPacketComposer
	{
		private string message;

		public GenericAlertComposer(string message, Session session)
		{
			this.message = message.Replace("%username%", session.Habbo.Username);
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.GenericAlertMessageComposer);
			message.WriteString(this.message);
			return message;
		}
	}
}
