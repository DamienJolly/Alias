using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Inventory.Composers
{
	public class AddBotComposer : IPacketComposer
	{
		private InventoryBot _bot;

		public AddBotComposer(InventoryBot bot)
		{
			_bot = bot;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.AddBotMessageComposer);
			message.WriteInteger(_bot.Id);
			message.WriteString(_bot.Name);
			message.WriteString(_bot.Motto);
			message.WriteString(_bot.Gender.ToLower());
			message.WriteString(_bot.Look);
			message.WriteBoolean(true);
			return message;
		}
	}
}
