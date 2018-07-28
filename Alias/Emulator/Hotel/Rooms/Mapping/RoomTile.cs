using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms.Items;
using Alias.Emulator.Hotel.Rooms.Users;

namespace Alias.Emulator.Hotel.Rooms.Mapping
{
    class RoomTile
    {
		private Room room;
		public TilePosition Position
		{
			get; set;
		}

		public List<RoomUser> Entities
		{
			get; set;
		}

		public List<RoomItem> Items
		{
			get; set;
		}

		public RoomTileState State
		{
			get; set;
		}

		public RoomTile(Room room, TilePosition position)
		{
			this.room = room;
			this.Position = position;
			this.Entities = new List<RoomUser>();
			this.Items = new List<RoomItem>();
			this.State = RoomTileState.OPEN;
		}

		public void AddEntity(RoomUser entity)
		{
			if (this.Position.X == this.room.Model.Door.X && this.Position.Y == this.room.Model.Door.Y)
			{
				return;
			}

			this.Entities.Add(entity);
		}

		public bool HasEntity(RoomUser entity)
		{
			return this.Entities.Contains(entity);
		}
		
		public void RemoveEntity(RoomUser entity)
		{
			this.Entities.Remove(entity);
		}

		public bool IsValidTile(RoomUser user)
		{
			if (this.Entities.Count > 0)
			{
				return user != null && this.HasEntity(user);
			}

			// todo: furni check

			return true;
		}
	}
}
