using System;
using Alias.Emulator.Hotel.Misc.Composers;
using Alias.Emulator.Hotel.Navigator;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Users.Currency;
using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Hotel.Users.Messenger;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users
{
	public sealed class Habbo : IDisposable
	{
		public int Id = 0;
		public string Username = "Unknown";
		public string Mail = "Default@email.com";
		public string Look = "";
		public string Gender = "M";
		public string Motto = "Hello world!";
		public int Rank = 1;
		public int ClubLevel = 1;
		public int Credits = 9999;
		public bool Disconnecting = false;
		public bool Muted = false;
		public Room CurrentRoom = null;
		public NavigatorPreference NavigatorPreference;

		private Messenger.Messenger messenger;
		private Inventory.Inventory inventory;
		private Currency.Currency currency;

		public Habbo()
		{

		}

		public void Init()
		{
			this.currency = new Currency.Currency(this);
			CurrencyDatabase.InitCurrency(this.currency);
			this.inventory = new Inventory.Inventory(this);
			InventoryDatabase.InitInventory(this.inventory);
			this.messenger = new Messenger.Messenger(this);
			MessengerDatabase.InitMessenger(this.messenger);
			this.Messenger().UpdateStatus(true);
			this.NavigatorPreference = NavigatorDatabase.Preference(this.Id);
			this.NavigatorPreference.NavigatorSearches = NavigatorDatabase.ReadSavedSearches(this.Id);
		}

		public void OnDisconnect()
		{
			this.Disconnecting = true;
			CurrencyDatabase.SaveCurrencies(this.currency);
		}

		public Session Session()
		{
			return SessionManager.SessionById(this.Id);
		}

		public void Notification(string text)
		{
			this.Session().Send(new GenericAlertComposer(text, Session()));
		}

		public Inventory.Inventory Inventory()
		{
			return this.inventory;
		}

		public Currency.Currency Currency()
		{
			return this.currency;
		}

		public Messenger.Messenger Messenger()
		{
			return this.messenger;
		}

		public void Dispose()
		{
			this.Username = null;
			this.Mail = null;
			this.Look = null;
			this.Motto = null;
			this.Gender = null;
			this.currency.Dispose();
		}
	}
}
