using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Inventory.Composers
{
	public class RemoveBotComposer : IPacketComposer
	{
		private InventoryBot _bot;

		public RemoveBotComposer(InventoryBot bot)
		{
			_bot = bot;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RemoveBotMessageComposer);
			message.WriteInteger(_bot.Id);
			return message;
		}
	}
}
