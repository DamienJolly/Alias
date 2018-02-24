namespace Alias.Emulator.Network.Messages.Headers
{
	public class Incoming
	{
		// Users

			// Handshake
			public const int ReleaseVersionMessageEvent = 4000;
			public const int SecureLoginMessageEvent = 2419;
			public const int MachineIDMessageEvent = 2490;
			public const int RequestUserDataMessageEvent = 357;
			public const int VersionCheckMessageEvent = -1;

		// Landing
		public const int HotelViewDataMessageEvent = 2912;
		public const int HotelViewMessageEvent = 105;
		public const int HotelViewRequestBonusRareMessageEvent = 957;
		public const int RequestNewsListMessageEvent = 1827;
	}
}
