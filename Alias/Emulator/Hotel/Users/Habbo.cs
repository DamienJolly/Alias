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
	public class Habbo : HabboData
	{
		/// <summary>
		/// Mark the Habbo as disconnecting.
		/// </summary>
		private Boolean _disconnecting = false;
		
		public Room CurrentRoom { get; set; } = null;
		public NavigatorPreference NavigatorPreference { get; set; }
		public UserSettings Settings { get; set; }
		public MessengerComponent Messenger { get; set; }
		public InventoryComponent Inventory { get; set; }
		public CurrencyComponent Currency { get; set; }
		public AchievementComponent Achievements { get; set; }
		public BadgeComponent Badges { get; set; }
		
		/// <summary>
		/// Initializes a new Habbo instance.
		/// </summary>
		public Habbo()
		{

		}
		
		/// <summary>
		/// The session used for this active user.
		/// </summary>
		public Session Session
		{
			get { return SessionManager.SessionById(this.Id); }
		}

		/// <summary>
		/// Checks to see if the user is disconnecting.
		/// </summary>
		public bool IsDisconnecting
		{
			get { return this._disconnecting; }
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
			this._disconnecting = true;
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
			return Alias.GetServer().GetPermissionManager().HasPermission(this.Rank, param);
		}
	}
}
