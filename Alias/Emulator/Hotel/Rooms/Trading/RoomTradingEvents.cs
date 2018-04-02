using Alias.Emulator.Hotel.Rooms.Trading.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Rooms.Trading
{
    public class RoomTradingEvents
    {
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.TradeStartMessageEvent, new TradeStartEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.TradeOfferItemMessageEvent, new TradeOfferItemEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.TradeOfferMultipleItemsMessageEvent, new TradeOfferMultipleItemsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.TradeCancelOfferItemMessageEvent, new TradeCancelOfferItemEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.TradeAcceptMessageEvent, new TradeAcceptEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.TradeUnAcceptMessageEvent, new TradeUnAcceptEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.TradeConfirmMessageEvent, new TradeConfirmEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.TradeCloseMessageEvent, new TradeCloseEvent());
		}
	}
}
