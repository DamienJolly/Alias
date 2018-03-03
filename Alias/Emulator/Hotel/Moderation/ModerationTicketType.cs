namespace Alias.Emulator.Hotel.Moderation
{
	public enum ModerationTicketType
	{
		NORMAL,
		NORMAL_UNKNOWN,
		AUTOMATIC,
		AUTOMATIC_IM,
		GUIDE_SYSTEM,
		IM,
		ROOM,
		PANIC,
		GUARDIAN,
		AUTOMATIC_HELPER,
		DISCUSSION,
		SELFIE,
		POTATO,
		PHOTO,
		AMBASSADOR
	}

	public class ModerationTicketTypes
	{
		public static ModerationTicketType GetTypeFromInt(int type)
		{
			switch (type)
			{
				case 1:
					return ModerationTicketType.NORMAL;
				case 2:
					return ModerationTicketType.NORMAL_UNKNOWN;
				case 3:
					return ModerationTicketType.AUTOMATIC;
				case 4:
					return ModerationTicketType.AUTOMATIC_IM;
				case 5:
					return ModerationTicketType.GUIDE_SYSTEM;
				case 6:
					return ModerationTicketType.IM;
				case 7:
					return ModerationTicketType.ROOM;
				case 8:
					return ModerationTicketType.PANIC;
				case 9:
					return ModerationTicketType.GUARDIAN;
				case 10:
					return ModerationTicketType.AUTOMATIC_HELPER;
				case 11:
					return ModerationTicketType.DISCUSSION;
				case 12:
					return ModerationTicketType.SELFIE;
				case 13:
					return ModerationTicketType.POTATO;
				case 14:
					return ModerationTicketType.PHOTO;
				case 15:
					return ModerationTicketType.AMBASSADOR;
				default:
					return ModerationTicketType.NORMAL;
			}
		}

		public static int GetIntFromType(ModerationTicketType type)
		{
			switch (type)
			{
				case ModerationTicketType.NORMAL:
					return 1;
				case ModerationTicketType.NORMAL_UNKNOWN:
					return 2;
				case ModerationTicketType.AUTOMATIC:
					return 3;
				case ModerationTicketType.AUTOMATIC_IM:
					return 4;
				case ModerationTicketType.GUIDE_SYSTEM:
					return 5;
				case ModerationTicketType.IM:
					return 6;
				case ModerationTicketType.ROOM:
					return 7;
				case ModerationTicketType.PANIC:
					return 8;
				case ModerationTicketType.GUARDIAN:
					return 9;
				case ModerationTicketType.AUTOMATIC_HELPER:
					return 10;
				case ModerationTicketType.DISCUSSION:
					return 11;
				case ModerationTicketType.SELFIE:
					return 12;
				case ModerationTicketType.POTATO:
					return 13;
				case ModerationTicketType.PHOTO:
					return 14;
				case ModerationTicketType.AMBASSADOR:
					return 15;
				default:
					return 1;
			}
		}
	}
}
