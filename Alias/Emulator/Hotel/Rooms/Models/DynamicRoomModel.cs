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
