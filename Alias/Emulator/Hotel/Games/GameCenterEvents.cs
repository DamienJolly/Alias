using Alias.Emulator.Hotel.Games.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Games
{
    class GameCenterEvents
    {
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.GameCenterRequestGamesMessageEvent, new GameCenterRequestGamesEvent());
		}
	}
}
