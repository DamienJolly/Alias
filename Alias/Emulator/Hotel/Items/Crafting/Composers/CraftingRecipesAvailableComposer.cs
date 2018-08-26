using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Items.Crafting.Composers
{
    class CraftingRecipesAvailableComposer : IPacketComposer
	{
		private int count;
		private bool found;

		public CraftingRecipesAvailableComposer(int count, bool found)
		{
			this.count = count;
			this.found = found;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.CraftingRecipesAvailableMessageComposer);
			message.WriteInteger((this.found ? -1 : 0) + this.count);
			message.WriteBoolean(this.found);
			return message;
		}
	}
}
