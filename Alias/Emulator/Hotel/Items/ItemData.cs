namespace Alias.Emulator.Hotel.Items
{
	public class ItemData
	{
		public int Id
		{
			get; set;
		}

		public string Name
		{
			get; set;
		}

		public int Width
		{
			get; set;
		}

		public int Length
		{
			get; set;
		}

		public double Height
		{
			get; set;
		}

		public bool CanSit
		{
			get; set;
		}

		public bool CanLay
		{
			get; set;
		}

		public bool CanWalk
		{
			get; set;
		}

		public int Modes
		{
			get; set;
		} = 2;

		public bool CanStack
		{
			get; set;
		} = true;
	}
}
