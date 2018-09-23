using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using System.Collections.Generic;

namespace Alias.Emulator.Hotel.Players.Wardrobe.Composers
{
	class UserWardrobeComposer : IPacketComposer
	{
		private ICollection<WardrobeItem> _items;

		public UserWardrobeComposer(ICollection<WardrobeItem> items)
		{
			_items = items;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserWardrobeMessageComposer);
			message.WriteInteger(1); // can use wardrobe
			message.WriteInteger(_items.Count);
			foreach (WardrobeItem item in _items)
			{
				message.WriteInteger(item.SlotId);
				message.WriteString(item.Figure);
				message.WriteString("M");
			}
			return message;
		}
	}
}
