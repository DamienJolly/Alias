using System;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Network.Messages
{
	public class EmptyMessageEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			Console.WriteLine("Unregistered Event with Id " + message.Id + " handled by the Placeholder MessageEvent.");
		}
	}
}
