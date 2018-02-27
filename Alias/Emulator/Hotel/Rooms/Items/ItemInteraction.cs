namespace Alias.Emulator.Hotel.Rooms.Items
{
	public enum ItemInteraction
	{
		DEFAULT,
		DICE
	}

	public class ItemInteractions
	{
		public static ItemInteraction GetInteractionFromString(string interaction)
		{
			switch (interaction)
			{
				case "default": default: return ItemInteraction.DEFAULT;
			}
		}
	}
}
