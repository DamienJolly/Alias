using System;
using Alias.Emulator.Hotel.Misc.Composers;
using Alias.Emulator.Hotel.Navigator;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Users.Achievements;
using Alias.Emulator.Hotel.Users.Badges;
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
		public int HomeRoom = 0;
		public int AchievementScore = 0;
		public bool Disconnecting = false;
		public bool Muted = false;
		public Room CurrentRoom = null;
		public NavigatorPreference NavigatorPreference;
		public UserSettings Settings;

		private Messenger.Messenger messenger;
		private Inventory.Inventory inventory;
		private Currency.Currency currency;
		private Achievements.Achievement achievements;
		private BadgeComponent badges;

		public Habbo()
		{

		}

		public void Init()
		{
			this.currency = new Currency.Currency(this);
			CurrencyDatabase.InitCurrency(this.currency);
			this.badges = new BadgeComponent(this);
			this.inventory = new Inventory.Inventory(this);
			InventoryDatabase.InitInventory(this.inventory);
			this.messenger = new Messenger.Messenger(this);
			MessengerDatabase.InitMessenger(this.messenger);
			this.achievements = new Achievements.Achievement(this);
			AchievementDatabase.InitAchievements(this.achievements);
			this.Messenger().UpdateStatus(true);
			this.NavigatorPreference = NavigatorDatabase.Preference(this.Id);
			this.NavigatorPreference.NavigatorSearches = NavigatorDatabase.ReadSavedSearches(this.Id);
			this.Settings = UserDatabase.Settings(this.Id);
		}

		public void OnDisconnect()
		{
			this.Disconnecting = true;
			if (this.Settings != null)
			{
				UserDatabase.UpdateSettings(this.Settings, this.Id);
			}
			if (this.achievements != null)
			{
				AchievementDatabase.SaveAchievements(this.achievements);
			}
			if (this.currency != null)
			{
				CurrencyDatabase.SaveCurrencies(this.currency);
			}
		}

		public Session Session()
		{
			return SessionManager.SessionById(this.Id);
		}

		public void Notification(string text)
		{
			this.Session().Send(new GenericAlertComposer(text, Session()));
		}

		public BadgeComponent GetBadgeComponent()
		{
			return this.badges;
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

		public Achievements.Achievement Achievements()
		{
			return this.achievements;
		}

		public void Dispose()
		{
			this.Username = null;
			this.Mail = null;
			this.Look = null;
			this.Motto = null;
			this.Gender = null;
			this.Settings = null;
			this.currency.Dispose();
			this.achievements.Dispose();
		}
	}
}
