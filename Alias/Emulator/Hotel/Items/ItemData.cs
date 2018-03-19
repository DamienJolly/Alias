using Alias.Emulator.Hotel.Rooms.Items;

namespace Alias.Emulator.Hotel.Items
{
	public class ItemData
	{
		public int Id
		{
			get; set;
		}

		public int SpriteId
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

		public string ExtraData
		{
			get; set;
		}

		public string Type
		{
			get; set;
		}

		public int Modes
		{
			get; set;
		} = 5;

		public bool CanStack
		{
			get; set;
		} = true;
		
		public WiredInteraction WiredInteraction
		{
			get; set;
		} = WiredInteraction.DEFAULT;

		public ItemInteraction Interaction
		{
			get; set;
		}

		public bool IsWired()
		{
			return Interaction == ItemInteraction.WIRED_CONDITION ||
				Interaction == ItemInteraction.WIRED_TRIGGER ||
				Interaction == ItemInteraction.WIRED_EFFECT;
		}
	}
}
