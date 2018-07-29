using Alias.Emulator.Hotel.Rooms.Items;
using Alias.Emulator.Hotel.Rooms.Users;

namespace Alias.Emulator.Hotel.Rooms.Mapping
{
	class RoomMapping
	{
		private Room room;

		public RoomTile[,] Tiles
		{
			get; set;
		}

		public int SizeX
		{
			get; set;
		}

		public int SizeY
		{
			get; set;
		}

		public RoomMapping(Room room)
		{
			this.room = room;
		}

		public void RegenerateCollisionMap()
		{
			string[] split = room.Model.Map.Replace("\n", "").Split('\r');
			this.SizeY = split.Length;
			this.SizeX = split[0].Length;
			this.Tiles = new RoomTile[this.SizeX, this.SizeY];
			for (int y = 0; y < this.SizeY; y++)
			{
				for (int x = 0; x < this.SizeX; x++)
				{
					char position = split[y][x];
					RoomTile tile = new RoomTile(room, new TilePosition(x, y, Alias.ParseChar(position)));
					if (position == 'x')
					{
						tile.State = RoomTileState.CLOSED;
					}
					this.Tiles[x, y] = tile;
				}
			}

			this.RegenerateEntityCollision();
			this.RegenerateItemsCollision();
		}

		public void RegenerateEntityCollision()
		{
			foreach (RoomTile tile in this.Tiles)
			{
				tile.Entities.Clear();
			}

			foreach (RoomUser user in this.room.UserManager.Users)
			{
				RoomTile tile = Tiles[user.Position.X, user.Position.Y];

				if (tile == null)
				{
					continue;
				}

				tile.AddEntity(user);
			}
		}

		public void RegenerateItemsCollision()
		{
			foreach (RoomTile tile in this.Tiles)
			{
				tile.Items.Clear();
			}

			foreach (RoomItem item in this.room.ItemManager.Items)
			{
				AddItem(item);
			}
		}

		public bool CanStackAt(int targertX, int targetY, RoomItem item)
		{
			for (int x = targertX; x <= targertX + (item.Position.Rotation == 0 || item.Position.Rotation == 4 ? item.ItemData.Width : item.ItemData.Length) - 1; x++)
			{
				for (int y = targetY; y <= targetY + (item.Position.Rotation == 0 || item.Position.Rotation == 4 ? item.ItemData.Length : item.ItemData.Width) - 1; y++)
				{
					if (!this.Tiles[x, y].CanStack(item))
					{
						return false;
					}
				}
			}
			return true;
		}

		public void AddItem(RoomItem item)
		{
			System.Console.WriteLine(item.Position.Rotation);
			for (int x = item.Position.X; x <= item.Position.X + (item.Position.Rotation == 0 || item.Position.Rotation == 4 ? item.ItemData.Width : item.ItemData.Length) - 1; x++)
			{
				for (int y = item.Position.Y; y <= item.Position.Y + (item.Position.Rotation == 0 || item.Position.Rotation == 4 ? item.ItemData.Length : item.ItemData.Width) - 1; y++)
				{
					this.Tiles[x, y].AddItem(item);
				}
			}
		}

		public void RemoveItem(RoomItem item)
		{
			for (int x = item.Position.X; x <= item.Position.X + (item.Position.Rotation == 0 || item.Position.Rotation == 4 ? item.ItemData.Width : item.ItemData.Length) - 1; x++)
			{
				for (int y = item.Position.Y; y <= item.Position.Y + (item.Position.Rotation == 0 || item.Position.Rotation == 4 ? item.ItemData.Length : item.ItemData.Width) - 1; y++)
				{
					this.Tiles[x, y].RemoveItem(item);
				}
			}
		}
	}
}
