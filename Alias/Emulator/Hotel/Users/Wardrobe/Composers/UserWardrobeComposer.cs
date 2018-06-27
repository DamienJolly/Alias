using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using System.Collections.Generic;

namespace Alias.Emulator.Hotel.Users.Wardrobe.Composers
{
	class UserWardrobeComposer : IPacketComposer
	{
		private ICollection<WardrobeItem> items;

		public UserWardrobeComposer(ICollection<WardrobeItem> items)
		{
			this.items = items;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserWardrobeMessageComposer);
			message.WriteInteger(1); // can use wardrobe
			message.WriteInteger(this.items.Count);
			foreach (WardrobeItem item in items)
			{
				message.WriteInteger(item.SlotId);
				message.WriteString(item.Figure);
				message.WriteString("M");
			}
			return message;
		}
	}
}
