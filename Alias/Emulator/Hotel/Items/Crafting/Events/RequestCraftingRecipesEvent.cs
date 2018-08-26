using System.Linq;
using Alias.Emulator.Hotel.Items.Crafting.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Rooms.Items;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Items.Crafting.Events
{
    class RequestCraftingRecipesEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			int itemId = message.PopInt();
			RoomItem item = room.ItemManager.GetItem(itemId);
			if (item == null)
			{
				return;
			}

			if (!Alias.Server.ItemManager.Crafting.TryGetCraftingTable(item.ItemData.Id, out CraftingTable table))
			{
				return;
			}

			System.Console.WriteLine(table.Recipes.Values.Count + " " + table.Ingredients.Count);
			//todo: user recipies
			session.Send(new CraftableProductsComposer(table.Recipes.Values.ToList(), table.Ingredients));
		}
	}
}
