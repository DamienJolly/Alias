using Alias.Emulator.Hotel.Rooms.Trading.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Rooms.Trading
{
    public class RoomTradingEvents
    {
		public static void Register()
		{
			Alias.GetServer().GetPacketManager().Register(Incoming.TradeStartMessageEvent, new TradeStartEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.TradeOfferItemMessageEvent, new TradeOfferItemEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.TradeOfferMultipleItemsMessageEvent, new TradeOfferMultipleItemsEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.TradeCancelOfferItemMessageEvent, new TradeCancelOfferItemEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.TradeAcceptMessageEvent, new TradeAcceptEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.TradeUnAcceptMessageEvent, new TradeUnAcceptEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.TradeConfirmMessageEvent, new TradeConfirmEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.TradeCloseMessageEvent, new TradeCloseEvent());
		}
	}
}
