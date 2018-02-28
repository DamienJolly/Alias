using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Network.Messages
{
	public class EmptyMessageEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			Logging.Debug("Unregistered Event with Id " + message.Id + " handled by the Placeholder MessageEvent.");
		}
	}
}
