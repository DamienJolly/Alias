using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Currency.Composers
{
	class UserCreditsComposer : IPacketComposer
	{
		private Habbo habbo;

		public UserCreditsComposer(Habbo habbo)
		{
			this.habbo = habbo;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserCreditsMessageComposer);
			message.WriteString(habbo.Credits + ".0");
			return message;
		}
	}
}
