using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Navigator.Composers
{
	public class NavigatorLiftedRoomsComposer : MessageComposer
	{
		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.NavigatorLiftedRoomsMessageComposer);
			result.Int(0);
			return result;
		}
	}
}
