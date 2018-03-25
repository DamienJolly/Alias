using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Inventory.Composers
{
	public class RemoveHabboItemComposer : IPacketComposer
	{
		private int itemId;

		public RemoveHabboItemComposer(int itemId)
		{
			this.itemId = itemId;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.RemoveHabboItemMessageComposer);
			message.Int(this.itemId);
			return message;
		}
	}
}
