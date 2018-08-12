using Alias.Emulator.Hotel.Rooms.Items;
using Alias.Emulator.Hotel.Rooms.Mapping;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Users.Composers
{
	class RoomUserOnRollerComposer : IPacketComposer
	{
		private RoomUser roomUser;
		private RoomItem roller;
		private RoomTile oldTile;
		private RoomTile newTile;
		private double offset;

		public RoomUserOnRollerComposer(RoomUser roomUser, RoomItem roller, RoomTile oldTile, RoomTile newTile, double offset = 0)
		{
			this.roomUser = roomUser;
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
			message.WriteInteger(0);
			message.WriteInteger(this.roller.Id);
			message.WriteInteger(2);
			message.WriteInteger(this.roomUser.VirtualId);
			message.WriteString(this.roomUser.Position.Z.ToString());
			message.WriteString((this.roomUser.Position.Z + this.offset).ToString());

			//todo: walkon walkoff interaction

			this.oldTile.RemoveEntity(this.roomUser);
			this.roomUser.Position.X = this.newTile.Position.X;
			this.roomUser.Position.Y = this.newTile.Position.Y;
			//todo: get proper player height
			this.roomUser.Position.Z = this.roomUser.Position.Z + this.offset;
			this.newTile.AddEntity(this.roomUser);

			return message;
		}
	}
}
