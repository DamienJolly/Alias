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
			// todo:
		}
	}
}
