using Alias.Emulator.Hotel.Users.Wardrobe.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Users.Wardrobe
{
    class WardrobeEvents
    {
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.UserSaveLookMessageEvent, new UserSaveLookEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.SaveWardrobeMessageEvent, new SaveWardrobeEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestUserWardrobeMessageEvent, new RequestUserWardrobeEvent());
		}
    }
}
