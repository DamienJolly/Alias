using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms.Items;
using Alias.Emulator.Hotel.Rooms.Users;

namespace Alias.Emulator.Hotel.Rooms.Models
{
	public class DynamicRoomModel
	{
		public Room Room
		{
			get; set;
		}

		public RoomModel BaseModel
		{
			get; set;
		}

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

		public double GetTileHeight(int x, int y, RoomItem item = null)
		{
			double height = this.Tiles[x, y].Z;

			if (item == null)
			{
				item = this.GetTopItemAt(x, y);
			}

			if (item != null)
			{
				height = height + item.Position.Z + item.ItemData.Height;
			}

			return height;
		}

		public bool ValidTile(int x, int y, bool goal = false)
		{
			if (!this.CanSitAt(this.GetItemsAt(x, y)) || !goal)
			{
				RoomItem item = this.GetTopItemAt(x, y);
				if (item != null && (this.GetTileHeight(x, y, item) > 2 || !item.ItemData.CanWalk))
				{
					return false;
				}
			}

			if (this.HasHabbosAt(x, y))
			{
				return false;
			}

			return true;
		}

		public bool HasHabbosAt(int x, int y)
		{
			foreach (RoomUser user in this.Room.UserManager.Users)
			{
				if (user.Position.X == x && user.Position.Y == y)
				{
					return true;
				}
			}

			return false;
		}

		public bool CanSitAt(int x, int y, bool GoalAchieved = false)
		{
			if (this.HasHabbosAt(x, y) && !GoalAchieved)
			{
				return false;
			}

			return this.CanSitAt(this.GetItemsAt(x, y));
		}

		public bool CanStackAt(int x, int y, RoomItem item = null)
		{
			bool canStack = true;
			RoomItem stackItem = this.GetTopItemAt(x, y);

			if (stackItem != null && stackItem != item)
			{
				if (!stackItem.ItemData.CanStack)
				{
					canStack = false;
				}
			}

			if (canStack)
			{
				if ((item == null) || (!item.ItemData.CanLay && !item.ItemData.CanSit && !item.ItemData.CanWalk))
				{
					if (this.HasHabbosAt(x, y))
					{
						canStack = false;
					}
				}
			}

			return canStack;
		}

		private bool CanSitAt(List<RoomItem> items)
		{
			if (items == null)
			{
				return false;
			}

			RoomItem topItem = null;
			RoomItem lowestSitItem = null;
			bool canSitUnder = false;

			foreach (RoomItem item in items)
			{
				if ((lowestSitItem == null || lowestSitItem.Position.Z > item.Position.Z) && item.ItemData.CanSit)
				{
					lowestSitItem = item;
				}

				if (lowestSitItem != null)
				{
					if (item.Position.Z > lowestSitItem.Position.Z)
					{
						if (item.Position.Z - lowestSitItem.Position.Z > 1)
						{
							canSitUnder = true;
						}
						else
						{
							canSitUnder = false;
						}
					}
				}

				if (topItem == null || item.Position.Z > topItem.Position.Z)
				{
					topItem = item;
				}
			}

			if (topItem == null || lowestSitItem == null )
			{
				return false;
			}

			if (topItem == lowestSitItem)
			{
				return true;
			}

			return topItem.Position.Z <= lowestSitItem.Position.Z || (canSitUnder);
		}

		public List<RoomItem> GetItemsAt(int x, int y)
		{
			List<RoomItem> items = new List<RoomItem>();
			foreach (RoomItem item in this.Room.ItemManager.Items)
			{
				if (item.Position.X == x && item.Position.Y == y)
				{
					items.Add(item);
				}
				else
				{
					if (item.ItemData.Width <= 1 && item.ItemData.Length <= 1)
					{
						continue;
					}

					List<RoomTile> tiles = this.GetTilesAt(this.Tiles[item.Position.X, item.Position.Y], item.ItemData.Width, item.ItemData.Length, item.Position.Rotation);
					foreach (RoomTile tile in tiles)
					{
						if ((tile.X == x) && (tile.Y == y) && (!items.Contains(item)))
						{
							items.Add(item);
						}
					}
				}
			}

			return items;
		}

		public List<RoomTile> GetTilesAt(RoomTile tile, int width, int length, int rotation)
		{
			List<RoomTile> pointList = new List<RoomTile>();

			if (tile != null)
			{
				if (rotation == 0 || rotation == 4)
				{
					for (int i = tile.X; i <= (tile.X + (width - 1)); i++)
					{
						for (int j = tile.Y; j <= (tile.Y + (length - 1)); j++)
						{
							RoomTile t = this.Tiles[i, j];

							if (t != null)
							{
								pointList.Add(t);
							}
						}
					}
				}
				else if (rotation == 2 || rotation == 6)
				{
					for (int i = tile.X; i <= (tile.X + (length - 1)); i++)
					{
						for (int j = tile.Y; j <= (tile.Y + (width - 1)); j++)
						{
							RoomTile t = this.Tiles[i, j];

							if (t != null)
							{
								pointList.Add(t);
							}
						}
					}
				}
			}

			return pointList;
		}

		public RoomItem GetLowestChair(int x, int y)
		{
			RoomItem lowestChair = null;

			foreach (RoomItem item in this.GetItemsAt(x, y))
			{
				if (item.ItemData.CanSit)
				{
					if (lowestChair == null || item.Position.Z < lowestChair.Position.Z)
					{
						lowestChair = item;
					}
				}

				if (lowestChair != null)
				{
					if (item.Position.Z > lowestChair.Position.Z && item.Position.Z - lowestChair.Position.Z < 1.5)
					{
						lowestChair = null;
					}
				}
			}

			return lowestChair;
		}

		public RoomItem GetTopItemAt(int x, int y)
		{
			RoomItem item = null;
			foreach (RoomItem habboItem in this.Room.ItemManager.Items)
			{
				try
				{
					if (habboItem.Position.X == x && habboItem.Position.Y == y)
					{
						if (item == null || (habboItem.Position.Z + habboItem.ItemData.Height) > (item.Position.Z + item.ItemData.Height))
						{
							item = habboItem;
						}
					}
					else
					{
						if (habboItem.ItemData.Width <= 1 && habboItem.ItemData.Length <= 1)
						{
							continue;
						}

						List<RoomTile> tiles = this.GetTilesAt(this.Tiles[habboItem.Position.X, habboItem.Position.Y], habboItem.ItemData.Width, habboItem.ItemData.Length, habboItem.Position.Rotation);
						foreach (RoomTile tile in tiles)
						{
							if (tile.X == x && tile.Y == y)
							{
								if (item == null || item.Position.Z < habboItem.Position.Z)
								{
									item = habboItem;
								}
							}
						}
					}
				}
				catch
				{
					break;
				}
			}

			return item;
		}

		public DynamicRoomModel(Room room)
		{
			this.Room = room;
			this.BaseModel = room.Model;
			string[] split = BaseModel.Map.Replace("\n", "").Split('\r');
			int ylength = split.Length;
			int xlength = split[0].Length;
			this.Tiles = new RoomTile[xlength, ylength];
			this.SizeX = xlength;
			this.SizeY = ylength;
			for (int y = 0; y < ylength; y++)
			{
				for (int x = 0; x < xlength; x++)
				{
					char position = split[y][x];
					RoomTile tile = new RoomTile();
					tile.X = x;
					tile.Y = y;
					tile.Z = AliasEnvironment.ParseChar(position);
					if (position == 'x')
					{
						tile.State = TileState.CLOSED;
						tile.Z = 0.0;
					}
					this.Tiles[x, y] = tile;
				}
			}
		}
	}
}
