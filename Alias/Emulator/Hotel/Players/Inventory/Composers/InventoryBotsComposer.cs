using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Inventory.Composers
{
	public class InventoryBotsComposer : IPacketComposer
	{
		private readonly ICollection<InventoryBot> _bots;

		public InventoryBotsComposer(ICollection<InventoryBot> bots)
		{
			_bots = bots;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.InventoryBotsMessageComposer);
			message.WriteInteger(_bots.Count);
			foreach (InventoryBot bot in _bots)
			{
				message.WriteInteger(bot.Id);
				message.WriteString(bot.Name);
				message.WriteString(bot.Motto);
				message.WriteString(bot.Gender.ToLower());
				message.WriteString(bot.Look);
			}
			return message;
		}
	}
}
