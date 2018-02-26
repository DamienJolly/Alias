namespace Alias.Emulator.Hotel.Catalog
{
	public enum CatalogLayout
	{
		/**
		 * Root page. Do not use!
		 */
		ROOT,
		DEFAULT_3x3,
		GUILD_FURNI,
		GUILDS,
		GUILD_FORUM,
		INFO_DUCKETS,
		INFO_RENTABLES,
		INFO_LOYALTY,
		LOYALTY_VIP_BUY,
		BOTS,
		PETS,
		PETS2,
		PETS3,
		CLUB_GIFT,
		FRONTPAGE,
		BADGE_DISPLAY,
		SPACES_NEW,
		SOUNDMACHINE,
		INFO_PETS,
		CLUB_BUY,
		ROOMADS,
		TROPHIES,
		SINGLE_BUNDLE,
		MARKETPLACE,
		MARKETPLACE_OWN_ITEMS,
		RECYCLER,
		RECYCLER_INFO,
		RECYCLER_PRIZES,
		SOLD_LTD_ITEMS,
		DEFAULT_3x3_COLOR_GROUPING,
		RECENT_PURCHASES,
		ROOM_BUNDLE,
		PETCUSTOMIZATION,
		VIP_BUY,
		FRONTPAGE_FEATURED,
		BUILDERS_CLUB_ADDONS,
		BUILDERS_CLUB_FRONTPAGE,
		BUILDERS_CLUB_LOYALTY,

		plasto
	}

	public class CatalogLayouts
	{
		public static CatalogLayout GetLayoutFromString(string layout)
		{
			switch (layout)
			{
				case "frontpage": return CatalogLayout.FRONTPAGE;
				case "badge_display": return CatalogLayout.BADGE_DISPLAY;
				case "spaces_new": return CatalogLayout.SPACES_NEW;
				case "trophies": return CatalogLayout.TROPHIES;
				case "bots": return CatalogLayout.BOTS;
				case "club_buy": return CatalogLayout.CLUB_BUY;
				case "club_gift": return CatalogLayout.CLUB_GIFT;
				case "sold_ltd_items": return CatalogLayout.SOLD_LTD_ITEMS;
				case "single_bundle": return CatalogLayout.SINGLE_BUNDLE;
				case "roomads": return CatalogLayout.ROOMADS;
				case "recycler": return CatalogLayout.RECYCLER;
				case "recycler_info": return CatalogLayout.RECYCLER_INFO;
				case "recycler_prizes": return CatalogLayout.RECYCLER_PRIZES;
				case "marketplace": return CatalogLayout.MARKETPLACE;
				case "marketplace_own_items": return CatalogLayout.MARKETPLACE_OWN_ITEMS;
				case "info_duckets": return CatalogLayout.INFO_DUCKETS;
				case "info_pets": return CatalogLayout.INFO_PETS;
				case "info_rentables": return CatalogLayout.INFO_RENTABLES;
				case "info_loyalty": return CatalogLayout.INFO_LOYALTY;
				case "loyalty_vip_buy": return CatalogLayout.LOYALTY_VIP_BUY;
				case "guilds": return CatalogLayout.GUILDS;
				case "guild_furni": return CatalogLayout.GUILD_FURNI;
				case "guild_forum": return CatalogLayout.GUILD_FORUM;
				case "pets": return CatalogLayout.PETS;
				case "pets2": return CatalogLayout.PETS2;
				case "pets3": return CatalogLayout.PETS3;
				case "soundmachine": return CatalogLayout.SOUNDMACHINE;
				case "default_3x3_color_grouping": return CatalogLayout.DEFAULT_3x3_COLOR_GROUPING;
				case "recent_purchases": return CatalogLayout.RECENT_PURCHASES;
				case "room_bundle": return CatalogLayout.ROOM_BUNDLE;
				case "petcustomization": return CatalogLayout.PETCUSTOMIZATION;
				case "vip_buy": return CatalogLayout.VIP_BUY;
				case "frontpage_featured": return CatalogLayout.FRONTPAGE_FEATURED;
				case "builders_club_addons": return CatalogLayout.BUILDERS_CLUB_ADDONS;
				case "builders_club_frontpage": return CatalogLayout.BUILDERS_CLUB_FRONTPAGE;
				case "builders_club_loyalty": return CatalogLayout.BUILDERS_CLUB_LOYALTY;
				case "default_3x3": default: return CatalogLayout.DEFAULT_3x3;
			}
		}
	}
}
