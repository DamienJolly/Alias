using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class RoomAccessDeniedComposer : IMessageComposer
	{
		string Username;

		public RoomAccessDeniedComposer(string username)
		{
			this.Username = username;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomAccessDeniedMessageComposer);
			result.String(this.Username);
			return result;
		}
	}
}
