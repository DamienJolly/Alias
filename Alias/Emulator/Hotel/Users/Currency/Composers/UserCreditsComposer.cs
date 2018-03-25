using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Currency.Composers
{
	public class UserCreditsComposer : IPacketComposer
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
