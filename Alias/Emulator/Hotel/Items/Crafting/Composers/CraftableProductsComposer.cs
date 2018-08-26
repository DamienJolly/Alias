using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Items.Crafting.Composers
{
    class CraftingRecipeComposer : IPacketComposer
	{
		private CraftingRecipe recipe;

		public CraftingRecipeComposer(CraftingRecipe recipe)
		{
			this.recipe = recipe;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.CraftingRecipeMessageComposer);
			message.WriteInteger(this.recipe.Ingredients.Count);
			foreach (KeyValuePair<string, int> ingredient in this.recipe.Ingredients)
			{
				message.WriteInteger(ingredient.Value);
				message.WriteString(ingredient.Key);
			}
			return message;
		}
	}
}
