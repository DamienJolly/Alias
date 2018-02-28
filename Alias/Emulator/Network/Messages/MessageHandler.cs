using System.Collections.Generic;
using Alias.Emulator.Hotel.Achievements;
using Alias.Emulator.Hotel.Catalog;
using Alias.Emulator.Hotel.Landing;
using Alias.Emulator.Hotel.Navigator;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Users;

namespace Alias.Emulator.Network.Messages
{
	public class MessageHandler
	{
		private static Dictionary<int, IMessageEvent> Events;
		private static IMessageEvent EmptyMessageEvent;

		public static void Initialize()
		{
			MessageHandler.Events = new Dictionary<int, IMessageEvent>();
			MessageHandler.EmptyMessageEvent = new EmptyMessageEvent();

			AchievementEvents.Register();
			CatalogEvents.Register();
			UserEvents.Register();
			RoomEvents.Register();
			LandingEvents.Register();
			NavigatorEvents.Register();
		}

		public static void Register(int Id, IMessageEvent evnt)
		{
			MessageHandler.Events.Add(Id, evnt);
		}

		public static IMessageEvent Event(int Id)
		{
			if (MessageHandler.Events.ContainsKey(Id))
			{
				return MessageHandler.Events[Id];
			}
			return MessageHandler.EmptyMessageEvent;
		}
	}
}
