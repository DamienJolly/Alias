using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Inventory.Composers
{
	public class AddBotComposer : IPacketComposer
	{
		InventoryBots bot;

		public AddBotComposer(InventoryBots bot)
		{
			this.bot = bot;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.AddBotMessageComposer);
			message.WriteInteger(bot.Id);
			message.WriteString(bot.Name);
			message.WriteString(bot.Motto);
			message.WriteString(bot.Gender.ToLower());
			message.WriteString(bot.Look);
			message.WriteBoolean(true);
			return message;
		}
	}
}
