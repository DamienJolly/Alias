using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Navigator.Composers
{
	public class PrivateRoomsComposer : IPacketComposer
	{
		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.PrivateRoomsMessageComposer);
			result.Int(2);
			result.String("");
			result.Int(0);
			result.Boolean(true);
			result.Int(0);
			result.String("A");
			result.String("B");
			result.Int(1);
			result.String("C");
			result.String("D");
			result.Int(1);
			result.Int(1);
			result.Int(1);
			result.String("E");
			return result;
		}
	}
}
