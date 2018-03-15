using Alias.Emulator.Hotel.Rooms.Items;

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

		public int BehaviourData
		{
			get; set;
		}

		public WiredInteraction WiredInteraction
		{
			get; set;
		} = WiredInteraction.DEFAULT;

		public ItemInteraction Interaction
		{
			get; set;
		}

		public string Type
		{
			get; set;
		} = "s";

		public int Modes
		{
			get; set;
		} = 2;

		public bool CanStack
		{
			get; set;
		} = true;

		public bool IsWired()
		{
			return Interaction == ItemInteraction.WIRED_CONDITION ||
				Interaction == ItemInteraction.WIRED_TRIGGER ||
				Interaction == ItemInteraction.WIRED_EFFECT;
		}
	}
}
