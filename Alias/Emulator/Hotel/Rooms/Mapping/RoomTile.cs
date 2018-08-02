using System.Collections.Generic;
using System.Linq;
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

		public void AddItem(RoomItem item)
		{
			this.Items.Add(item);
		}

		public void HasItem(RoomItem item)
		{
			this.Items.Contains(item);
		}

		public void RemoveItem(RoomItem item)
		{
			this.Items.Remove(item);
		}

		public RoomItem TopItem
		{
			get
			{
				RoomItem topItem = null;
				foreach (RoomItem item in this.Items)
				{
					if (topItem == null || (item.Position.Z + item.ItemData.Height) > (topItem.Position.Z + topItem.ItemData.Height))
					{
						topItem = item;
					}
				}
				return topItem;
			}
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

		public bool IsValidTile(RoomUser user, bool final)
		{
			if (this.Entities.Count > 0)
			{
				return user != null && this.HasEntity(user);
			}

			if (this.Items.Count > 0)
			{
				if (this.TopItem.ItemData.CanWalk || ((this.TopItem.ItemData.CanSit && final)))
				{
					return true;
				}
				else if (this.TopItem.ItemData.CanLay && final)
				{
					if (this.TopItem.Position.X == this.Position.X && this.TopItem.Position.Y == this.Position.Y)
					{
						// todo: finish
						return true;
					}
					return false;
				}
				else
				{
					return false;
				}
			}

			return true;
		}

		public double Height => this.TopItem != null ? this.TopItem.ItemData.Height + this.TopItem.Position.Z + this.Position.Z : this.Position.Z;

		public bool CanStack(RoomItem item)
		{
			if (this.Entities.Count > 0)
			{
				if (this.Items.Count > 0)
				{
					return this.TopItem == item && item.ItemData.CanSit;
				}
			}

			if (this.Items.Count > 0)
			{
				return this.TopItem == item || this.TopItem.ItemData.CanStack;
			}

			return true;
		}

		public bool TilesAdjecent(RoomTile targetTile)
		{
			int dx = this.Position.X - targetTile.Position.X;
			int dy = this.Position.Y - targetTile.Position.Y;

			return (dx * dx) + (dy * dy) <= 2;
		}
	}
}
