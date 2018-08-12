using Alias.Emulator.Hotel.Rooms.Mapping;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Items.Composers
{
	class FloorItemOnRollerComposer : IPacketComposer
	{
		private RoomItem item;
		private RoomItem roller;
		private RoomTile oldTile;
		private RoomTile newTile;
		private double offset;

		public FloorItemOnRollerComposer(RoomItem item, RoomItem roller, RoomTile oldTile, RoomTile newTile, double offset = 0)
		{
			this.item = item;
			this.roller = roller;
			this.oldTile = oldTile;
			this.newTile = newTile;
			this.offset = offset;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.ObjectOnRollerMessageComposer);
			message.WriteInteger(this.oldTile.Position.X);
			message.WriteInteger(this.oldTile.Position.Y);
			message.WriteInteger(this.newTile.Position.X);
			message.WriteInteger(this.newTile.Position.Y);
			message.WriteInteger(1);
			message.WriteInteger(this.item.Id);
			message.WriteString(this.item.Position.Z.ToString());
			message.WriteString((this.item.Position.Z + this.offset).ToString());
			message.WriteInteger(this.roller.Id);

			this.item.Room.Mapping.RemoveItem(this.item);
			this.item.Position.X = this.newTile.Position.X;
			this.item.Position.Y = this.newTile.Position.Y;
			this.item.Position.Z = this.item.Position.Z + this.offset;
			this.item.Room.Mapping.AddItem(this.item);

			return message;
		}
	}
}
