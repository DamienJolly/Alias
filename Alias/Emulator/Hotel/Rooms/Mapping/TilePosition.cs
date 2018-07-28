namespace Alias.Emulator.Hotel.Rooms.Mapping
{
    class TilePosition
    {
		public int X
		{
			get; set;
		}

		public int Y
		{
			get; set;
		}

		public double Z
		{
			get; set;
		}

		public TilePosition(int x, int y, double z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}
	}
}
