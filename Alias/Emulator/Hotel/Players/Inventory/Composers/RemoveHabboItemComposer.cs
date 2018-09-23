using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Inventory.Composers
{
	public class RemovePlayerItemComposer : IPacketComposer
	{
		private int itemId;

		public RemovePlayerItemComposer(int itemId)
		{
			this.itemId = itemId;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RemovePlayerItemMessageComposer);
			message.WriteInteger(this.itemId);
			return message;
		}
	}
}
