using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Currency.Composers
{
	public class UserCreditsComposer : IMessageComposer
	{
		private Habbo habbo;

		public UserCreditsComposer(Habbo habbo)
		{
			this.habbo = habbo;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.UserCreditsMessageComposer);
			result.String(habbo.Credits + ".0");
			return result;
		}
	}
}
