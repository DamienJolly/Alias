using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Inventory.Composers
{
	public class InventoryRefreshComposer : IPacketComposer
	{
		public ServerMessage Compose()
		{
			return new ServerMessage(Outgoing.InventoryRefreshMessageComposer);
		}
	}
}
