using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Misc.Composers
{
	public class GenericErrorComposer : IPacketComposer
	{
		int ErrorCode;

		public GenericErrorComposer(int error)
		{
			this.ErrorCode = error;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.GenericErrorMessageComposer);
			message.WriteInteger(this.ErrorCode);
			return message;
		}
	}
}
