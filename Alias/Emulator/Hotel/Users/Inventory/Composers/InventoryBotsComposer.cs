using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Inventory.Composers
{
	public class InventoryBotsComposer : IPacketComposer
	{
		private List<InventoryBots> bots;

		public InventoryBotsComposer(List<InventoryBots> bots)
		{
			this.bots = bots;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.InventoryBotsMessageComposer);
			message.WriteInteger(this.bots.Count);
			this.bots.ForEach(bot =>
			{
				message.WriteInteger(bot.Id);
				message.WriteString(bot.Name);
				message.WriteString(bot.Motto);
				message.WriteString(bot.Gender.ToLower());
				message.WriteString(bot.Look);
			});
			return message;
		}
	}
}
