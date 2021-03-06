using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	class RoomSettingsComposer : IPacketComposer
	{
		private Room room;

		public RoomSettingsComposer(Room room)
		{
			this.room = room;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomSettingsMessageComposer);
			message.WriteInteger(this.room.Id);
			message.WriteString(this.room.RoomData.Name);
			message.WriteString(this.room.RoomData.Description);
			message.WriteInteger((int)this.room.RoomData.DoorState);
			message.WriteInteger(this.room.RoomData.Category);
			message.WriteInteger(this.room.RoomData.MaxUsers);
			message.WriteInteger(this.room.Mapping.SizeX * this.room.Mapping.SizeY > 100 ? 50 : 25);
			message.WriteInteger(this.room.RoomData.Tags.Count);
			this.room.RoomData.Tags.ForEach(t => message.WriteString(t));
			message.WriteInteger((int)this.room.RoomData.TradeState);
			message.WriteInteger(this.room.RoomData.Settings.AllowPets ? 1 : 0);
			message.WriteInteger(this.room.RoomData.Settings.AllowPetsEat ? 1 : 0);
			message.WriteInteger(this.room.RoomData.Settings.RoomBlocking ? 1 : 0);
			message.WriteInteger(this.room.RoomData.Settings.HideWalls ? 1 : 0);
			message.WriteInteger(this.room.RoomData.Settings.WallHeight);
			message.WriteInteger(this.room.RoomData.Settings.FloorSize);
			message.WriteInteger(this.room.RoomData.Settings.ChatMode);
			message.WriteInteger(this.room.RoomData.Settings.ChatSize);
			message.WriteInteger(this.room.RoomData.Settings.ChatSpeed);
			message.WriteInteger(this.room.RoomData.Settings.ChatDistance);
			message.WriteInteger(this.room.RoomData.Settings.ChatFlood);
			message.WriteBoolean(false); 
			message.WriteInteger(this.room.RoomData.Settings.WhoMutes);
			message.WriteInteger(this.room.RoomData.Settings.WhoKicks);
			message.WriteInteger(this.room.RoomData.Settings.WhoBans);
			return message;
		}
	}
}
