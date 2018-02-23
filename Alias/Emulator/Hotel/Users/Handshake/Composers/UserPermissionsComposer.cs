using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Handshake.Composers
{
	public class UserPermissionsComposer : MessageComposer
	{
		int Rank;

		public UserPermissionsComposer(int rank)
		{
			this.Rank = rank;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.UserPermissionsMessageComposer);
			message.Int(2); //club level
			message.Int(this.Rank);
			message.Boolean(false); //ambassador
			return message;
		}
	}
}
