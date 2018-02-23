using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Misc.Composers
{
	public class GenericAlertComposer : MessageComposer
	{
		private string message;

		public GenericAlertComposer(string message, Session session)
		{
			this.message = message.Replace("%username%", session.Habbo().Username);
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.GenericAlertMessageComposer);
			result.String(this.message);
			return result;
		}
	}
}
