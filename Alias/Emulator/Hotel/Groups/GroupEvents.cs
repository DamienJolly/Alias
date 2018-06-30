using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Hotel.Groups.Events;

namespace Alias.Emulator.Hotel.Groups
{
    public class GroupEvents
    {
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestGroupInfoMessageEvent, new RequestGroupInfoEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestGroupBuyRoomsMessageEvent, new RequestGroupBuyRoomsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestGroupPartsMessageEvent, new RequestGroupPartsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestGroupBuyMessageEvent, new RequestGroupBuyEvent());
		}
	}
}
