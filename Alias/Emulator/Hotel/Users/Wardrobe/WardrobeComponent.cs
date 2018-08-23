using System.Collections.Generic;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Users.Wardrobe
{
	sealed class WardrobeComponent
    {
		/// <summary>
		/// Wardrobe Items with Item ID > WardrobeItem.
		/// </summary>
		private Dictionary<int, WardrobeItem> wardobeItems;

		private Habbo habbo;

		/// <summary>
		/// When the users figure was last updated.
		/// </summary>
		private double figureLastUpdated = 0;

		/// <summary>
		/// How long to wait until the figure can be changed again? (should match the clients setting)
		/// </summary>
		private static int figureWaitTimeInSec = 6;

		/// <summary>
		/// Initializes the WardrobeComponent.
		/// </summary>
		/// <param name="Habbo">h.</param>
		public WardrobeComponent(Habbo h)
		{
			this.wardobeItems = WardrobeDatabase.ReadWardrobeItems(h.Id);

			this.habbo = h;
		}

		/// <summary>
		/// Checks to see the number of available slots.
		/// </summary>
		public int SlotsAvailable
		{
			get
			{
				return 10;//(habbo.HasPermission("club_regular")) ? 5 : (habbo.HasPermission("club_vip")) ? 10 : 0;
			}
		}

		/// <summary>
		/// Checks if the figure can be changed.
		/// </summary>
		public bool CanChangeFigure
		{
			get
			{
				return (this.figureLastUpdated - UnixTimestamp.Now) > 0 ? false : true;
			}
		}

		/// <summary>
		/// Sets that the users figure has been updated.
		/// </summary>
		public void SetFigureUpdated()
		{
			this.figureLastUpdated = UnixTimestamp.Now + figureWaitTimeInSec;
		}

		/// <summary>
		/// Try to retrieve a WardrobeItem based on the slot id.
		/// </summary>
		/// <param name="Id"></param>
		/// <param name="Item"></param>
		/// <returns></returns>
		public bool TryGet(int Id, out WardrobeItem Item)
		{
			return this.wardobeItems.TryGetValue(Id, out Item);
		}

		public bool TryAdd(int Id, WardrobeItem Item)
		{
			if (this.wardobeItems.ContainsKey(Id))
			{
				return false;
			}

			this.wardobeItems.Add(Id, Item);
			return true;
		}

		public IDictionary<int, WardrobeItem> WardobeItems => this.wardobeItems;
	}
}
