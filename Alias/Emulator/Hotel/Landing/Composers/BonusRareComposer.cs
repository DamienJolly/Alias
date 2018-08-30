using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Landing.Composers
{
	class BonusRareComposer : IPacketComposer
	{
		private LandingBonusRare bonusRare;

		public BonusRareComposer(LandingBonusRare bonusRare)
		{
			this.bonusRare = bonusRare;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.BonusRareMessageComposer);
			message.WriteString(this.bonusRare.Name);
			message.WriteInteger(this.bonusRare.Prize.Id);
			message.WriteInteger(this.bonusRare.Goal);
			message.WriteInteger(0);
			return message;
		}
	}
}
