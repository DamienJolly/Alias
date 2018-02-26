namespace Alias.Emulator.Network.Messages.Headers
{
	public class Incoming
	{
		// Users
		public const int RequestUserProfileMessageEvent = 3265;

			// Messenger
			public const int RequestInitFriendsMessageEvent = 2781;
			public const int SearchUserMessageEvent = 1210;
			public const int FriendRequestMessageEvent = 3157;
			public const int AcceptFriendRequestMessageEvent = 137;
			public const int DeclineFriendRequestMessageEvent = 2890;
			public const int RequestFriendRequestsMessageEvent = 2448;
			public const int FriendPrivateMessageMessageEvent = 3567;
			public const int RemoveFriendMessageEvent = 1689;
			public const int InviteFriendsMessageEvent = 1276;
			public const int RequestFriendsMessageEvent = 1523;

			// Currency
			public const int RequestUserCreditsMessageEvent = 273;

			// Handshake
			public const int ReleaseVersionMessageEvent = 4000;
			public const int SecureLoginMessageEvent = 2419;
			public const int MachineIDMessageEvent = 2490;
			public const int RequestUserDataMessageEvent = 357;
			public const int VersionCheckMessageEvent = -1;

			// Inventory
			public const int RequestInventoryItemsMessageEvent = 3150;

		// Rooms
		public const int RequestRoomDataMessageEvent = 2230;
		public const int RequestRoomLoadMessageEvent = 2312;
		public const int RequestRoomHeightmapMessageEvent = 3898;

			// Users
			public const int RoomUserWalkMessageEvent = 3320;
			public const int RoomUserTalkMessageEvent = 1314;
			public const int RoomUserShoutMessageEvent = 2085;
			public const int RoomUserWhisperMessageEvent = 1543;

		// Landing
		public const int HotelViewDataMessageEvent = 2912;
		public const int HotelViewMessageEvent = 105;
		public const int HotelViewRequestBonusRareMessageEvent = 957;
		public const int RequestNewsListMessageEvent = 1827;

		// Navigator
		public const int RequestNavigatorDataMessageEvent = 2110;
		public const int SaveWindowSettingsMessageEvent = 3159;
		public const int SearchRoomsMessageEvent = 249;
		public const int RequestRoomCategoriesMessageEvent = 3027;
		public const int RequestNavigatorSettingsMessageEvent = 1782;
		public const int RequestPromotedRoomsMessageEvent = 2908;
		public const int AddSavedSearchMessageEvent = 2226;
		public const int RemoveSavedSearchMessageEvent = 1954;

		// Catalog
		public const int RequestCatalogIndexMessageEvent = 2529;
		public const int RequestCatalogModeMessageEvent = 1195;
		public const int RequestCatalogPageMessageEvent = 412;
		public const int RequestDiscountMessageEvent = 223;
		public const int CatalogBuyItemMessageEvent = 3492;
	}
}
