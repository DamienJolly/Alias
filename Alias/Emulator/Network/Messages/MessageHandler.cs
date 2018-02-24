using System.Collections.Generic;
using Alias.Emulator.Hotel.Users.Handshake;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Utilities;

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

			HandshakeEvents.Register();
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
