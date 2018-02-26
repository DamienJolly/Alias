using System.Collections.Generic;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Inventory.Composers
{
	public class InventoryRefreshComposer : MessageComposer
	{
		public ServerMessage Compose()
		{
			return new ServerMessage(Outgoing.InventoryRefreshMessageComposer);
		}
	}
}
