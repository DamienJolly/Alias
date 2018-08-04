namespace Alias.Emulator.Hotel.Rooms.Items
{
	public enum ItemInteraction
	{
		DEFAULT,
		DICE,
		EXCHANGE,
		DIAMOND_EXCHANGE,
		POINTS_EXCHANGE,
		BOT,
		VENDING_MACHINE,
		TILE_EFFECT,
		TROPHY,

		WIRED_TRIGGER,
		WIRED_EFFECT,
		WIRED_CONDITION
	}

	public class ItemInteractions
	{
		public static ItemInteraction GetInteractionFromString(string interaction)
		{
			switch (interaction)
			{
				case "bot": return ItemInteraction.BOT;
				case "wired_trigger": return ItemInteraction.WIRED_TRIGGER;
				case "wired_effect": return ItemInteraction.WIRED_EFFECT;
				case "wired_condition": return ItemInteraction.WIRED_CONDITION;
				case "exchange": return ItemInteraction.EXCHANGE;
				case "diamond_exchange": return ItemInteraction.DIAMOND_EXCHANGE;
				case "points_exchange": return ItemInteraction.POINTS_EXCHANGE;
				case "dice": return ItemInteraction.DICE;
				case "vending": return ItemInteraction.VENDING_MACHINE;
				case "effect": return ItemInteraction.TILE_EFFECT;
				case "trophy": return ItemInteraction.TROPHY;
				case "default": default: return ItemInteraction.DEFAULT;
			}
		}
	}
}
