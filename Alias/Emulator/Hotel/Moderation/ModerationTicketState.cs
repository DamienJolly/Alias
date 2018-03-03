namespace Alias.Emulator.Hotel.Moderation
{
	public enum ModerationTicketState
	{
		CLOSED,
		OPEN,
		PICKED
	}

	public class ModerationTicketStates
	{
		public static ModerationTicketState GetStateFromInt(int state)
		{
			switch (state)
			{
				case 0:
					return ModerationTicketState.CLOSED;
				case 1:
					return ModerationTicketState.OPEN;
				case 2:
					return ModerationTicketState.PICKED;
				default:
					return ModerationTicketState.OPEN;
			}
		}

		public static int GetIntFromState(ModerationTicketState state)
		{
			switch (state)
			{
				case ModerationTicketState.CLOSED:
					return 0;
				case ModerationTicketState.OPEN:
					return 1;
				case ModerationTicketState.PICKED:
					return 2;
				default:
					return 1;
			}
		}
	}
}
