using Alias.Emulator.Hotel.Rooms.Items.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Rooms.Items
{
	public class RoomItemEvents
	{
		public static void Register()
		{
			Alias.GetServer().GetPacketManager().Register(Incoming.RotateMoveItemMessageEvent, new RotateMoveItemEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RoomPlaceItemMessageEvent, new RoomPlaceItemEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RoomPickupItemMessageEvent, new RoomPickupItemEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.ToggleFloorItemMessageEvent, new ToggleFloorItemEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RedeemItemMessageEvent, new RedeemItemEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.TriggerDiceMessageEvent, new TriggerDiceEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.CloseDiceMessageEvent, new CloseDiceEvent());
		}
	}
}
