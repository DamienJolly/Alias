using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Landing.Composers
{
	class BonusRareComposer : IPacketComposer
	{
		private LandingBonusRare bonusRare;
		private int progress;

		public BonusRareComposer(LandingBonusRare bonusRare, int progress)
		{
			this.bonusRare = bonusRare;
			this.progress = progress;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.BonusRareMessageComposer);
			message.WriteString(this.bonusRare.Name);
			message.WriteInteger(this.bonusRare.Prize.Id);
			message.WriteInteger(this.bonusRare.Goal);
			message.WriteInteger(this.bonusRare.Goal - this.progress);
			return message;
		}
	}
}
