using System.Collections.Generic;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Inventory.Composers
{
	public class InventoryBotsComposer : IMessageComposer
	{
		private List<InventoryBots> bots;

		public InventoryBotsComposer(List<InventoryBots> bots)
		{
			this.bots = bots;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.InventoryBotsMessageComposer);
			message.Int(this.bots.Count);
			this.bots.ForEach(bot =>
			{
				message.Int(bot.Id);
				message.String(bot.Name);
				message.String(bot.Motto);
				message.String(bot.Gender.ToLower());
				message.String(bot.Look);
			});
			return message;
		}
	}
}
