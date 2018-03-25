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

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.GenericErrorMessageComposer);
			message.Int(this.ErrorCode);
			return message;
		}
	}
}
