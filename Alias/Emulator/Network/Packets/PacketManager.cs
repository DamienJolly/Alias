using System.Collections.Generic;
using Alias.Emulator.Hotel.Achievements;
using Alias.Emulator.Hotel.Catalog;
using Alias.Emulator.Hotel.Landing;
using Alias.Emulator.Hotel.Moderation;
using Alias.Emulator.Hotel.Navigator;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Users;

namespace Alias.Emulator.Network.Packets
{
	sealed class PacketManager
	{
		private readonly Dictionary<int, IPacketEvent> _events;

		public PacketManager()
		{
			this._events = new Dictionary<int, IPacketEvent>();
		}

		public void Initialize()
		{
			ModerationEvents.Register();
			AchievementEvents.Register();
			CatalogEvents.Register();
			UserEvents.Register();
			RoomEvents.Register();
			LandingEvents.Register();
			NavigatorEvents.Register();
		}

		public void Register(int Id, IPacketEvent evnt)
		{
			this._events.Add(Id, evnt);
		}

		public IPacketEvent Event(int Id)
		{
			if (this._events.ContainsKey(Id))
			{
				return this._events[Id];
			}
			return new EmptyEvent();
		}
	}
}
