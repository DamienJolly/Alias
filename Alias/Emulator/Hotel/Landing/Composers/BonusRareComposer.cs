using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Landing.Composers
{
	public class BonusRareComposer : MessageComposer
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
