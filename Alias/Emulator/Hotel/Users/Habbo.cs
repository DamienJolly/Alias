using System;
using Alias.Emulator.Hotel.Misc.Composers;
using Alias.Emulator.Hotel.Navigator;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users
{
	public sealed class Habbo : IDisposable
	{
		public int Id = 1;
		public string Username = "Damien";
		public string Mail = "DamienJolly@hotmail.com";
		public string Look = "";
		public string Gender = "M";
		public string Motto = "Hello world!";
		public int Rank = 1;
		public int ClubLevel = 1;
		public int Credits = 9999;
		public int Points = 9999;
		public bool Disconnecting = false;
		public NavigatorPreference NavigatorPreference;
		private Inventory.Inventory inventory;

		public Room CurrentRoom
		{
			get; set;
		} = null;

		public Habbo()
		{

		}

		public void Init()
		{
			this.inventory = new Inventory.Inventory(this);
			InventoryDatabase.InitInventory(this.inventory);
			this.NavigatorPreference = NavigatorDatabase.Preference(this.Id);
			this.NavigatorPreference.NavigatorSearches = NavigatorDatabase.ReadSavedSearches(this.Id);
		}

		public void OnDisconnect()
		{
			this.Disconnecting = true;
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

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
