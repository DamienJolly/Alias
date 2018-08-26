using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Items.Crafting.Composers
{
    class CraftingResultComposer : IPacketComposer
	{
		private CraftingRecipe recipe;

		public CraftingResultComposer(CraftingRecipe recipe)
		{
			this.recipe = recipe;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.CraftingResultMessageComposer);
			message.WriteBoolean(recipe != null);
			if (recipe != null)
			{
				message.WriteString(this.recipe.Reward.Name);
				message.WriteString(this.recipe.Reward.Name);
			}
			return message;
		}
	}
}
