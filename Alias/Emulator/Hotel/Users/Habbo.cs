using System;
using Alias.Emulator.Hotel.Misc.Composers;
using Alias.Emulator.Hotel.Navigator;
using Alias.Emulator.Hotel.Permissions;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Hotel.Rooms.Users.Chat;
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
		public int Id { get; set; } = 0;
		public string Username { get; set; } = "Unknown";
		public string Mail { get; set; } = "Default@email.com";
		public string Look { get; set; } = "";
		public string Gender { get; set; } = "M";
		public string Motto { get; set; } = "Hello world!";
		public int Rank { get; set; } = 6;
		public int ClubLevel { get; set; } = 1;
		public int Credits { get; set; } = 9999;
		public int HomeRoom { get; set; } = 0;
		public int AchievementScore { get; set; } = 0;
		public bool Disconnecting { get; set; } = false;
		public bool Muted { get; set; } = false;
		public bool AllowTrading { get; set; } = true;
		public Room CurrentRoom { get; set; } = null;
		public NavigatorPreference NavigatorPreference { get; set; }
		public UserSettings Settings { get; set; }
		public MessengerComponent Messenger { get; set; }
		public InventoryComponent Inventory { get; set; }
		public CurrencyComponent Currency { get; set; }
		public AchievementComponent Achievements { get; set; }
		public BadgeComponent Badges { get; set; }

		public Session Session
		{
			get
			{
				return SessionManager.SessionById(this.Id);
			}
		}

		public Habbo()
		{

		}

		public void Init()
		{
			this.Currency = new CurrencyComponent(this);
			this.Badges = new BadgeComponent(this);
			this.Inventory = new InventoryComponent(this);
			this.Messenger = new MessengerComponent(this);
			this.Achievements = new AchievementComponent(this);

			this.Messenger.UpdateStatus(true);
			this.NavigatorPreference = NavigatorDatabase.Preference(this.Id);
			this.NavigatorPreference.NavigatorSearches = NavigatorDatabase.ReadSavedSearches(this.Id);
			this.Settings = UserDatabase.Settings(this.Id);
		}

		public void OnDisconnect()
		{
			this.Disconnecting = true;
			if (this.CurrentRoom != null)
			{
				this.CurrentRoom.UserManager.OnUserLeave(this.Session);
			}
			if (this.Settings != null)
			{
				UserDatabase.UpdateSettings(this.Settings, this.Id);
			}
			if (this.Achievements != null)
			{
				AchievementDatabase.SaveAchievements(this.Achievements);
			}
			if (this.Currency != null)
			{
				CurrencyDatabase.SaveCurrencies(this.Currency);
			}
		}

		public void Notification(string text, bool forced = false)
		{
			if (this.CurrentRoom != null && !forced)
			{
				RoomUser target = this.CurrentRoom.UserManager.UserByUserid(this.Id);
				target.OnChat(text, 0, ChatType.WHISPER, target);
			}
			else
			{
				this.Session.Send(new GenericAlertComposer(text, Session));
			}
		}

		public bool HasPermission(string param)
		{
			return PermissionManager.HasPermission(this.Rank, param);
		}

		public void Dispose()
		{
			this.Currency.Dispose();
			this.Achievements.Dispose();
		}
	}
}
