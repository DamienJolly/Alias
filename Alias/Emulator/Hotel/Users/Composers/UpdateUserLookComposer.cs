using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Composers
{
	public class UpdateUserLookComposer : IPacketComposer
	{
		private Habbo habbo;

		public UpdateUserLookComposer(Habbo habbo)
		{
			this.habbo = habbo;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.UpdateUserLookMessageComposer);
			result.String(this.habbo.Look);
			result.String(this.habbo.Gender);
			return result;
		}
	}
}
