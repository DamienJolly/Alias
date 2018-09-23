using Alias.Emulator.Hotel.Items.Crafting.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Items.Crafting.Events
{
    class CraftingAddRecipeEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Player.CurrentRoom;
			if (room == null)
			{
				return;
			}

			string recipeName = message.PopString();
			if (!Alias.Server.ItemManager.Crafting.TryGetRecipe(recipeName, out CraftingRecipe recipe))
			{
				return;
			}

			session.Send(new CraftingRecipeComposer(recipe));
		}
	}
}
