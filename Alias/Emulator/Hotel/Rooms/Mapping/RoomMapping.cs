using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms.Items;
using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Utilities;

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

			foreach (RoomEntity entity in this.room.EntityManager.Entities)
			{
				RoomTile tile = Tiles[entity.Position.X, entity.Position.Y];

				if (tile == null)
				{
					continue;
				}

				tile.AddEntity(entity);
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

		public List<RoomTile> GetTilesFromItem(int targetX, int targetY, RoomItem item)
		{
			List<RoomTile> tiles = new List<RoomTile>();
			for (int x = targetX; x <= targetX + (item.Position.Rotation == 0 || item.Position.Rotation == 4 ? item.ItemData.Width : item.ItemData.Length) - 1; x++)
			{
				for (int y = targetY; y <= targetY + (item.Position.Rotation == 0 || item.Position.Rotation == 4 ? item.ItemData.Length : item.ItemData.Width) - 1; y++)
				{
					tiles.Add(this.Tiles[x, y]);
				}
			}
			return tiles;
		}

		public RoomTile GetTileInFront(RoomTile tile, int rotation, int offset = 0)
		{
			int offsetX = 0;
			int offsetY = 0;

			switch (rotation)
			{
				case 0: offsetY--; break;
				case 1: offsetX++; offsetY--; break;
				case 2: offsetX++; break;
				case 3: offsetX++; offsetY++; break;
				case 4: offsetY++; break;
				case 5: offsetX--; offsetY++; break;
				case 6: offsetX--; break;
				case 7: offsetX--; offsetY--; break;
			}

			int x = tile.Position.X;
			int y = tile.Position.Y;

			for (int i = 0; i <= offset; i++)
			{
				x += offsetX;
				y += offsetY;
			}

			return this.Tiles[x, y];
		}

		public RoomTile RandomWalkableTile
		{
			get
			{
				for (int i = 0; i < 10; i++)
				{
					RoomTile tile = this.Tiles[Randomness.RandomNumber(this.SizeX), Randomness.RandomNumber(this.SizeY)];
					if (tile.IsValidTile(null, true))
					{
						return tile;
					}
				}

				return null;
			}
		}

		public bool CanStackAt(int targertX, int targetY, RoomItem item)
		{
			bool canStack = true;
			GetTilesFromItem(targertX, targetY, item).ForEach(tile =>
			{
				if (!tile.CanStack(item))
				{
					canStack = false;
				}
			});
			return canStack;
		}

		public void AddItem(RoomItem item)
		{
			GetTilesFromItem(item.Position.X, item.Position.Y, item).ForEach(tile => tile.AddItem(item));
		}

		public void RemoveItem(RoomItem item)
		{
			GetTilesFromItem(item.Position.X, item.Position.Y, item).ForEach(tile => tile.RemoveItem(item));
		}
	}
}
