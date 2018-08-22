using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Items.Composers
{
	class RoomWallItemsComposer : IPacketComposer
	{
		private List<RoomItem> items;

		public RoomWallItemsComposer(List<RoomItem> items)
		{
			this.items = items;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomWallItemsMessageComposer);
			message.WriteInteger(0); //TODO
						   //message.WriteInteger(0); //userId
						   //message.WriteString(""); //username
			message.WriteInteger(this.items.Count);
			this.items.ForEach(item =>
			{
				message.WriteString(item.Id + "");
				message.WriteInteger(item.ItemData.SpriteId);
				message.WriteString(item.Position.WallPosition);
				message.WriteString(item.Mode + "");
				message.WriteInteger(-1);
				message.WriteInteger(item.ItemData.Modes > 1 ? 1 : 0);
				message.WriteInteger(item.Owner);
			});
			return message;
		}
	}
}
