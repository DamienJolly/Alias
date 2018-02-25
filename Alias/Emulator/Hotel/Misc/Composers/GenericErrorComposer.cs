using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Misc.Composers
{
	public class GenericErrorComposer : MessageComposer
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
