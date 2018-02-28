using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Messenger.Composers
{
	public class RoomInviteComposer : IMessageComposer
	{
		private int Sender;
		private string Message;

		public RoomInviteComposer(int sender, string message)
		{
			this.Sender = sender;
			this.Message = message;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.RoomInviteMessageComposer);
			message.Int(this.Sender);
			message.String(this.Message);
			return message;
		}
	}
}
