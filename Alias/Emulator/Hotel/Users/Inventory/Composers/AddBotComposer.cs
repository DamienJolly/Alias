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

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.AddBotMessageComposer);
			message.Int(bot.Id);
			message.String(bot.Name);
			message.String(bot.Motto);
			message.String(bot.Gender.ToLower());
			message.String(bot.Look);
			message.Boolean(true);
			return message;
		}
	}
}
