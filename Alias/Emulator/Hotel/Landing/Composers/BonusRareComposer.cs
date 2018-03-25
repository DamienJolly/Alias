using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Landing.Composers
{
	public class BonusRareComposer : IPacketComposer
	{
		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.BonusRareMessageComposer);
			result.String("prizetrophy_breed_gold");
			result.Int(230);
			result.Int(120);
			result.Int(120);
			return result;
		}
	}
}
