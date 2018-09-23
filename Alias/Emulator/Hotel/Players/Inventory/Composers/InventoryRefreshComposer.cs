using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Inventory.Composers
{
	public class InventoryRefreshComposer : IPacketComposer
	{
		public ServerPacket Compose()
		{
			return new ServerPacket(Outgoing.InventoryRefreshMessageComposer);
		}
	}
}
