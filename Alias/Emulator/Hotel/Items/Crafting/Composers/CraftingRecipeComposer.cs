using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Items.Crafting.Composers
{
    class CraftableProductsComposer : IPacketComposer
	{
		private List<CraftingRecipe> recipes;
		private List<string> ingredients;

		public CraftableProductsComposer(List<CraftingRecipe> recipes, List<string> ingredients)
		{
			this.recipes = recipes;
			this.ingredients = ingredients;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.CraftableProductsMessageComposer);

			message.WriteInteger(this.recipes.Count);
			this.recipes.ForEach(recipe =>
			{
				message.WriteString(recipe.Reward.Name);
				message.WriteString(recipe.Reward.Name);
			});

			message.WriteInteger(this.ingredients.Count);
			this.ingredients.ForEach(ingredient =>
			{
				message.WriteString(ingredient);
			});

			return message;
		}
	}
}
