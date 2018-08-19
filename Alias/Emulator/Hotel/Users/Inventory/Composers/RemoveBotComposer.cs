using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Inventory.Composers
{
	public class RemoveBotComposer : IPacketComposer
	{
		InventoryBots bot;

		public RemoveBotComposer(InventoryBots bot)
		{
			this.bot = bot;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RemoveBotMessageComposer);
			message.WriteInteger(bot.Id);
			return message;
		}
	}
}
