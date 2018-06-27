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
			message.WriteInteger(Alias.Server.RoomManager.DoorToInt(this.room.RoomData.DoorState));
			message.WriteInteger(this.room.RoomData.Category);
			message.WriteInteger(this.room.RoomData.MaxUsers);
			message.WriteInteger(this.room.DynamicModel.SizeX * this.room.DynamicModel.SizeY > 100 ? 50 : 25);
			message.WriteInteger(this.room.RoomData.Tags.Count);
			this.room.RoomData.Tags.ForEach(t => message.WriteString(t));
			message.WriteInteger(Alias.Server.RoomManager.TradeToInt(this.room.RoomData.TradeState));
			message.WriteInteger(int.Parse(Alias.BoolToString(this.room.RoomData.Settings.AllowPets)));
			message.WriteInteger(int.Parse(Alias.BoolToString(this.room.RoomData.Settings.AllowPetsEat)));
			message.WriteInteger(int.Parse(Alias.BoolToString(this.room.RoomData.Settings.RoomBlocking)));
			message.WriteInteger(int.Parse(Alias.BoolToString(this.room.RoomData.Settings.HideWalls)));
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
