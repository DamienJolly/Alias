using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class RoomSettingsComposer : IMessageComposer
	{
		private Room room;

		public RoomSettingsComposer(Room room)
		{
			this.room = room;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomSettingsMessageComposer);
			result.Int(this.room.Id);
			result.String(this.room.RoomData.Name);
			result.String(this.room.RoomData.Description);
			result.Int(RoomManager.DoorToInt(this.room.RoomData.DoorState));
			result.Int(this.room.RoomData.Category);
			result.Int(this.room.RoomData.MaxUsers);
			result.Int(this.room.DynamicModel.SizeX * this.room.DynamicModel.SizeY > 100 ? 50 : 25);
			result.Int(this.room.RoomData.Tags.Count);
			this.room.RoomData.Tags.ForEach(t => result.String(t));
			result.Int(RoomManager.TradeToInt(this.room.RoomData.TradeState));
			result.Int(int.Parse(AliasEnvironment.BoolToString(this.room.RoomData.Settings.AllowPets)));
			result.Int(int.Parse(AliasEnvironment.BoolToString(this.room.RoomData.Settings.AllowPetsEat)));
			result.Int(int.Parse(AliasEnvironment.BoolToString(this.room.RoomData.Settings.RoomBlocking)));
			result.Int(int.Parse(AliasEnvironment.BoolToString(this.room.RoomData.Settings.HideWalls)));
			result.Int(this.room.RoomData.Settings.WallHeight);
			result.Int(this.room.RoomData.Settings.FloorSize);
			result.Int(this.room.RoomData.Settings.ChatMode);
			result.Int(this.room.RoomData.Settings.ChatSize);
			result.Int(this.room.RoomData.Settings.ChatSpeed);
			result.Int(this.room.RoomData.Settings.ChatDistance);
			result.Int(this.room.RoomData.Settings.ChatFlood);
			result.Boolean(true);
			result.Int(this.room.RoomData.Settings.WhoMutes);
			result.Int(this.room.RoomData.Settings.WhoKicks);
			result.Int(this.room.RoomData.Settings.WhoBans);
			return result;
		}
	}
}
