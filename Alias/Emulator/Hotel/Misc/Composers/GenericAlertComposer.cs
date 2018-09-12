using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Misc.Composers
{
	class GenericAlertComposer : IPacketComposer
	{
		private string message;

		public GenericAlertComposer(string message)
		{
			this.message = message;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.GenericAlertMessageComposer);
			message.WriteString(this.message);
			return message;
		}
	}
}
