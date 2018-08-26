using Alias.Emulator.Hotel.Items.Crafting.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Items.Crafting
{
    class CraftingEvents
    {
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestCraftingRecipesMessageEvent, new RequestCraftingRecipesEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.CraftingAddRecipeMessageEvent, new CraftingAddRecipeEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestCraftingRecipesAvailableMessageEvent, new RequestCraftingRecipesAvailableEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.CraftingCraftItemMessageEvent, new CraftingCraftItemEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.CraftingCraftSecretMessageEvent, new CraftingCraftSecretEvent());
		}
	}
}
