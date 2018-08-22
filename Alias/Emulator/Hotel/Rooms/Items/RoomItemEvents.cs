using Alias.Emulator.Hotel.Rooms.Items.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Rooms.Items
{
	public class RoomItemEvents
	{
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RotateMoveItemMessageEvent, new RotateMoveItemEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RoomPlaceItemMessageEvent, new RoomPlaceItemEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RoomPickupItemMessageEvent, new RoomPickupItemEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.ToggleFloorItemMessageEvent, new ToggleFloorItemEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RedeemItemMessageEvent, new RedeemItemEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.TriggerDiceMessageEvent, new TriggerDiceEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.CloseDiceMessageEvent, new CloseDiceEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.MoveWallItemMessageEvent, new MoveWallItemEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.ToggleWallItemMessageEvent, new ToggleWallItemEvent());
		}
	}
}
