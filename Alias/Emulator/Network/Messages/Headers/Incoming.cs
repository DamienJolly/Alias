namespace Alias.Emulator.Network.Messages.Headers
{
	public class Incoming
	{
		// Users
		public const int RequestUserProfileMessageEvent = 2023;
		public const int RequestMeMenuSettingsMessageEvent = 3537;
		public const int SaveUserVolumesMessageEvent = 2507;
		public const int SavePreferOldChatMessageEvent = 2213;
		public const int SaveIgnoreRoomInvitesMessageEvent = 3110;
		public const int SaveBlockCameraFollowMessageEvent = 191;
		public const int UsernameMessageEvent = 3822;
		public const int UserWearBadgeMessageEvent = 3804;
		public const int RequestWearingBadgesMessageEvent = 1318;
		public const int UserSaveLookMessageEvent = 1588;

			// Messenger
			public const int RequestInitFriendsMessageEvent = 1405;
			public const int SearchUserMessageEvent = 3483;
			public const int FriendRequestMessageEvent = 3779;
			public const int AcceptFriendRequestMessageEvent = 1879;
			public const int DeclineFriendRequestMessageEvent = 204;
			public const int RequestFriendRequestsMessageEvent = 2467;
			public const int FriendPrivateMessageMessageEvent = 2279;
			public const int RemoveFriendMessageEvent = 853;
			public const int InviteFriendsMessageEvent = 1280;
			public const int RequestFriendsMessageEvent = 1368;
			public const int FindNewFriendsMessageEvent = 1100;

			// Currency
			public const int RequestUserCreditsMessageEvent = 2109;

			// Handshake
			public const int ReleaseVersionMessageEvent = 4000;
			public const int SecureLoginMessageEvent = 1930;
			public const int MachineIDMessageEvent = 3465;
			public const int RequestUserDataMessageEvent = 3092;
			public const int VersionCheckMessageEvent = 410;

			// Inventory
			public const int RequestInventoryItemsMessageEvent = 2835;
			public const int RequestInventoryBadgesMessageEvent = 1023;

		// Rooms
		public const int RequestHeightmapMessageEvent = 2443;
		public const int RequestRoomDataMessageEvent = 425;
		public const int RequestRoomLoadMessageEvent = 3464;
		public const int RequestRoomHeightmapMessageEvent = 1583;
		public const int RequestRoomRightsMessageEvent = 3071;

			// Users
			public const int RoomUserWalkMessageEvent = 3155;
			public const int RoomUserTalkMessageEvent = 500;
			public const int RoomUserShoutMessageEvent = 3803;
			public const int RoomUserWhisperMessageEvent = 3485;
			public const int RoomUserActionMessageEvent = 2925;
			public const int RoomUserSitMessageEvent = 68;
			public const int RoomUserSignMessageEvent = 1146;
			public const int RoomUserLookAtPointMessageEvent = 258;
			public const int RoomUserDanceMessageEvent = 3911;
			public const int RoomUserGiveRightsMessageEvent = 3880;
			public const int RoomUserRemoveRightsMessageEvent = 404;

			// Trading
			public const int TradeStartMessageEvent = 3586;
			public const int TradeOfferItemMessageEvent = 2175;
			public const int TradeOfferMultipleItemsMessageEvent = 1725;
			public const int TradeCancelOfferItemMessageEvent = 2177;
			public const int TradeAcceptMessageEvent = 999;
			public const int TradeUnAcceptMessageEvent = 2851;
			public const int TradeConfirmMessageEvent = 727;
			public const int TradeCloseMessageEvent = 3602;

			// Items
			public const int RotateMoveItemMessageEvent = 1620;
			public const int RoomPlaceItemMessageEvent = 2187;
			public const int RoomPickupItemMessageEvent = 3036;
			public const int ToggleFloorItemMessageEvent = 1699;

		// Landing
		public const int HotelViewDataMessageEvent = 1579;
		public const int HotelViewMessageEvent = 3119;
		public const int HotelViewRequestBonusRareMessageEvent = 1019;
		public const int RequestNewsListMessageEvent = 1156;

		// Navigator
		public const int RequestNavigatorDataMessageEvent = 2142;
		public const int SaveWindowSettingsMessageEvent = 107;
		public const int SearchRoomsMessageEvent = 3612;
		public const int RequestRoomCategoriesMessageEvent = 3976;
		public const int RequestNavigatorSettingsMessageEvent = 708;
		public const int RequestPromotedRoomsMessageEvent = 2446;
		public const int AddSavedSearchMessageEvent = 117;
		public const int RemoveSavedSearchMessageEvent = 448;

		// Catalog
		public const int RequestCatalogIndexMessageEvent = 1191;
		public const int RequestCatalogModeMessageEvent = 2565;
		public const int RequestCatalogPageMessageEvent = 3547;
		public const int RequestDiscountMessageEvent = 703;
		public const int CatalogBuyItemMessageEvent = 2687;

		// Achievements
		public const int RequestAchievementsMessageEvent = 3280;
	}
}
