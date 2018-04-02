using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Items.Composers
{
	public class RoomFloorItemsComposer : IPacketComposer
	{
		private List<RoomItem> Items;

		public RoomFloorItemsComposer(List<RoomItem> items)
		{
			this.Items = items;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomFloorItemsMessageComposer);
			message.WriteInteger(0); //TODO
						   //message.WriteInteger(0); //userId
						   //message.WriteString(""); //username
			message.WriteInteger(this.Items.Count);
			this.Items.ForEach(item =>
			{
				message.WriteInteger(item.Id);
				message.WriteInteger(item.ItemData.SpriteId);
				message.WriteInteger(item.Position.X);
				message.WriteInteger(item.Position.Y);
				message.WriteInteger(item.Position.Rotation);
				message.WriteString(item.Position.Z.ToString());
				message.WriteString(item.ItemData.Height.ToString());
				message.WriteInteger(1);
				item.GetInteractor().Serialize(message, item);
				if (item.IsLimited)
				{
					message.WriteInteger(item.LimitedNumber);
					message.WriteInteger(item.LimitedStack);
				}
				message.WriteInteger(-1); // item Rent time
				message.WriteInteger((item.ItemData.Modes > 1) ? 1 : 0);
				message.WriteInteger(item.Owner); // Borrowed = -12345678
			});
			return message;
		}
	}
}
