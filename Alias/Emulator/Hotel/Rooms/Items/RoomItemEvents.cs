using Alias.Emulator.Hotel.Rooms.Items.Events;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;

namespace Alias.Emulator.Hotel.Rooms.Items
{
	public class RoomItemEvents
	{
		public static void Register()
		{
			MessageHandler.Register(Incoming.RotateMoveItemMessageEvent, new RotateMoveItemEvent());
			MessageHandler.Register(Incoming.RoomPlaceItemMessageEvent, new RoomPlaceItemEvent());
			MessageHandler.Register(Incoming.RoomPickupItemMessageEvent, new RoomPickupItemEvent());
			MessageHandler.Register(Incoming.ToggleFloorItemMessageEvent, new ToggleFloorItemEvent());
			MessageHandler.Register(Incoming.RedeemItemMessageEvent, new RedeemItemEvent());
		}
	}
}
