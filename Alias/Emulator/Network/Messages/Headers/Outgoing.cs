namespace Alias.Emulator.Network.Messages.Headers
{
	public class Outgoing
	{
		// Users
		public const int UserPerksMessageComposer = 2586;
		public const int UserDataMessageComposer = 2725;
		public const int UserPermissionsMessageComposer = 411;
		public const int UserHomeRoomMessageComposer = 2875;
		public const int UserClothesMessageComposer = 1450;
		public const int UserAchievementScoreMessageComposer = 1968;
		public const int UserClubMessageComposer = 954;
		public const int UserEffectsListMessageComposer = 340;

			// Handshake
			public const int DebugConsoleMessageComposer = 3284;
			public const int SecureLoginOKMessageComposer = 2491;
			public const int SessionRightsMessageComposer = 2033;
			public const int NewUserIdentityMessageComposer = 3738;
			public const int SomeConnectionMessageComposer = 3928;

		// Rooms
		public const int RoomDataMessageComposer = 687;
		public const int RoomOpenMessageComposer = 758;
		public const int RoomAccessDeniedMessageComposer = 878;
		public const int RoomModelMessageComposer = 2031;
		public const int RoomPaintMessageComposer = 2454;
		public const int RoomScoreMessageComposer = 482;
		public const int RoomEntryInfoMessageComposer = 749;
		public const int RoomFloorThicknessUpdatedMessageComposer = 3786;

			// Models
			public const int RoomHeightMapMessageComposer = 1301;
			public const int RoomRelativeMapMessageComposer = 2753;

			// Users
			public const int RoomUsersMessageComposer = 374;
			public const int RoomUserRemoveMessageComposer = 2661;
			public const int RoomUserStatusMessageComposer = 1640;
			public const int RoomUserTalkMessageComposer = 1446;
			public const int RoomUserShoutMessageComposer = 1036;
			public const int RoomUserWhisperMessageComposer = 2704;
		
		// Landing
		public const int HotelViewDataMessageComposer = 1745;
		public const int HallOfFameMessageComposer = 3005;
		public const int NewsListMessageComposer = 286;
		public const int BonusRareMessageComposer = 1533;
		public const int HotelViewMessageComposer = 1745;

		// Navigator
		public const int RoomCategoriesMessageComposer = 1562;
		public const int NavigatorSearchResultsMessageComposer = 2690;
		public const int NavigatorEventCategoriesMessageComposer = 3244;
		public const int PrivateRoomsMessageComposer = 52;
		public const int NavigatorMetaDataMessageComposer = 3052;
		public const int NavigatorLiftedRoomsMessageComposer = 3104;
		public const int NavigatorCollapsedCategoriesMessageComposer = 1543;
		public const int NavigatorSavedSearchesMessageComposer = 3984;
		public const int NavigatorSettingsMessageComposer = 518;

		// Catalog
		public const int CatalogModeMessageComposer = 3828;
		public const int CatalogPagesListMessageComposer = 1032;
		public const int CatalogPageMessageComposer = 804;
		public const int DiscountMessageComposer = 2347;

		// Misc
		public const int GenericAlertMessageComposer = 3801;
		public const int GenericErrorMessageComposer = 1600;

			// Unknown
			public const int UnknownComposer4 = 793;
			public const int UnknownComposer5 = 2833;
	}
}
