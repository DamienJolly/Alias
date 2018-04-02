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

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UpdateUserLookMessageComposer);
			message.WriteString(this.habbo.Look);
			message.WriteString(this.habbo.Gender);
			return message;
		}
	}
}
