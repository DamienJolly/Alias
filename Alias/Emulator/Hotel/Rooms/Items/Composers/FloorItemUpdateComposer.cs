using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Items.Composers
{
	public class FloorItemUpdateComposer : IPacketComposer
	{
		private RoomItem Item;

		public FloorItemUpdateComposer(RoomItem item)
		{
			this.Item = item;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.FloorItemUpdateMessageComposer);
			message.WriteInteger(this.Item.Id);
			message.WriteInteger(this.Item.ItemData.SpriteId);
			message.WriteInteger(this.Item.Position.X);
			message.WriteInteger(this.Item.Position.Y);
			message.WriteInteger(this.Item.Position.Rotation);
			message.WriteString(this.Item.Position.Z.ToString());
			message.WriteString(this.Item.ItemData.Height.ToString());
			message.WriteInteger(1);
			this.Item.GetInteractor().Serialize(message, this.Item);
			if (Item.IsLimited)
			{
				message.WriteInteger(Item.LimitedNumber);
				message.WriteInteger(Item.LimitedStack);
			}
			message.WriteInteger(-1); // item Rent time
			message.WriteInteger((this.Item.ItemData.Modes > 1) ? 1 : 0);
			message.WriteInteger(this.Item.Owner); // Borrowed = -12345678
			return message;
		}
	}
}
