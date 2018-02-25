namespace Alias.Emulator.Hotel.Rooms.Pathfinding
{
	public class Node
	{
		public int X
		{
			get; set;
		}

		public int Y
		{
			get; set;
		}

		public int CostsToStartPoint
		{
			get; set;
		} = int.MaxValue;

		public int FullCosts
		{
			get; set;
		} = int.MaxValue;

		public Node ParentNode
		{
			get; set;
		} = null;

		public bool Closed
		{
			get; set;
		} = false;

		public Node(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}
	}
}
