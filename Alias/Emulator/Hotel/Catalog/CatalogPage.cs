using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Catalog.Layouts;

namespace Alias.Emulator.Hotel.Catalog
{
	public class CatalogPage
	{
		public int Id
		{
			get; set;
		}

		public int ParentId
		{
			get; set;
		}

		public string Name
		{
			get; set;
		}

		public string Caption
		{
			get; set;
		}

		public int Icon
		{
			get; set;
		}

		public int Rank
		{
			get; set;
		}

		public int Order
		{
			get; set;
		}

		public List<CatalogItem> Items
		{
			get; set;
		}

		public string HeaderImage
		{
			get; set;
		}

		public string TeaserImage
		{
			get; set;
		}

		public string SpecialImage
		{
			get; set;
		}

		public string TextOne
		{
			get; set;
		}

		public string TextTwo
		{
			get; set;
		}

		public string TextDetails
		{
			get; set;
		}

		public string TextTeaser
		{
			get; set;
		}

		//todo: vip

		public CatalogLayout Layout
		{
			get; set;
		}

		public bool Enabled
		{
			get; set;
		}

		public bool Visible
		{
			get; set;
		}

		public List<int> OfferIds
		{
			get; set;
		} = new List<int>();

		public void AddOfferId(int offerId)
		{
			this.OfferIds.Add(offerId);
		}

		public CatalogItem GetCatalogItem(int itemId)
		{
			return this.Items.Where(item => item.Id == itemId).FirstOrDefault();
		}

		public ICatalogLayout GetLayout()
		{
			switch (this.Layout)
			{
				case CatalogLayout.FRONTPAGE: return new LayoutFrontpage();
				case CatalogLayout.BADGE_DISPLAY: return new LayoutBadgeDisplay();
				case CatalogLayout.SPACES_NEW: return new LayoutSpacesNew();
				case CatalogLayout.TROPHIES: return new LayoutTrophies();
				case CatalogLayout.BOTS: return new LayoutBots();
				case CatalogLayout.CLUB_BUY: return new LayoutClubBuy();
				case CatalogLayout.CLUB_GIFT: return new LayoutClubGift();
				case CatalogLayout.SOLD_LTD_ITEMS: return new LayoutSoldLTDItems();
				case CatalogLayout.SINGLE_BUNDLE: return new LayoutSingleBundle();
				case CatalogLayout.ROOMADS: return new LayoutRoomAds();
				case CatalogLayout.RECYCLER: return new LayoutRecycler();
				case CatalogLayout.RECYCLER_INFO: return new LayoutRecyclerInfo();
				case CatalogLayout.RECYCLER_PRIZES: return new LayoutRecyclerPrizes();
				case CatalogLayout.MARKETPLACE: return new LayoutMarketplace();
				case CatalogLayout.MARKETPLACE_OWN_ITEMS: return new LayoutMarketplaceOwnItems();
				case CatalogLayout.INFO_DUCKETS: return new LayoutInfoDuckets();
				case CatalogLayout.INFO_PETS: return new LayoutInfoPets();
				case CatalogLayout.INFO_RENTABLES: return new LayoutInfoRentables();
				case CatalogLayout.INFO_LOYALTY: return new LayoutInfoLoyalty();
				case CatalogLayout.LOYALTY_VIP_BUY: return new LayoutLoyaltyVIPBuy();
				case CatalogLayout.GUILDS: return new LayoutGuilds();
				case CatalogLayout.GUILD_FURNI: return new LayoutGuildFurniture();
				case CatalogLayout.GUILD_FORUM: return new LayoutGuildForum();
				case CatalogLayout.PETS: return new LayoutPets();
				case CatalogLayout.PETS2: return new LayoutPets2();
				case CatalogLayout.PETS3: return new LayoutPets3();
				case CatalogLayout.SOUNDMACHINE: return new LayoutSoundMachine();
				case CatalogLayout.DEFAULT_3x3_COLOR_GROUPING: return new LayoutColorGrouping();
				case CatalogLayout.ROOM_BUNDLE: return new LayoutRoomBundle();
				case CatalogLayout.PETCUSTOMIZATION: return new LayoutPetCustomization();
				case CatalogLayout.VIP_BUY: return new LayoutVIPBuy();
				case CatalogLayout.FRONTPAGE_FEATURED: return new LayoutFrontpageFeatured();
				case CatalogLayout.BUILDERS_CLUB_ADDONS: return new LayoutBuildersClubAddons();
				case CatalogLayout.BUILDERS_CLUB_FRONTPAGE: return new LayoutBuildersClubFrontpage();
				case CatalogLayout.BUILDERS_CLUB_LOYALTY: return new LayoutBuildersClubLoyalty();
				case CatalogLayout.DEFAULT_3x3: case CatalogLayout.RECENT_PURCHASES: default: return new LayoutDefault();
			}
		}
	}
}
