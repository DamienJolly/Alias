using Alias.Emulator.Hotel.Rooms.Trading.Events;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;

namespace Alias.Emulator.Hotel.Rooms.Trading
{
    public class RoomTradingEvents
    {
		public static void Register()
		{
			MessageHandler.Register(Incoming.TradeStartMessageEvent, new TradeStartEvent());
			MessageHandler.Register(Incoming.TradeOfferItemMessageEvent, new TradeOfferItemEvent());
			MessageHandler.Register(Incoming.TradeOfferMultipleItemsMessageEvent, new TradeOfferMultipleItemsEvent());
			MessageHandler.Register(Incoming.TradeCancelOfferItemMessageEvent, new TradeCancelOfferItemEvent());
			MessageHandler.Register(Incoming.TradeAcceptMessageEvent, new TradeAcceptEvent());
			MessageHandler.Register(Incoming.TradeUnAcceptMessageEvent, new TradeUnAcceptEvent());
			MessageHandler.Register(Incoming.TradeConfirmMessageEvent, new TradeConfirmEvent());
			MessageHandler.Register(Incoming.TradeCloseMessageEvent, new TradeCloseEvent());
		}
	}
}
