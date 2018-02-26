using System.Collections.Generic;
using Alias.Emulator.Hotel.Catalog;
using Alias.Emulator.Hotel.Landing;
using Alias.Emulator.Hotel.Navigator;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Users;

namespace Alias.Emulator.Network.Messages
{
	public class MessageHandler
	{
		private static Dictionary<int, MessageEvent> Events;
		private static MessageEvent EmptyMessageEvent;
		public static void Initialize()
		{
			MessageHandler.Events = new Dictionary<int, MessageEvent>();
			MessageHandler.EmptyMessageEvent = new EmptyMessageEvent();

			CatalogEvents.Register();
			UserEvents.Register();
			RoomEvents.Register();
			LandingEvents.Register();
			NavigatorEvents.Register();
		}

		public static void Register(int Id, MessageEvent evnt)
		{
			MessageHandler.Events.Add(Id, evnt);
		}

		public static MessageEvent Event(int Id)
		{
			if (MessageHandler.Events.ContainsKey(Id))
			{
				return MessageHandler.Events[Id];
			}
			return MessageHandler.EmptyMessageEvent;
		}
	}
}
