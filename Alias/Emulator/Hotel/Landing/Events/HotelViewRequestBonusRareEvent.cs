using Alias.Emulator.Hotel.Landing.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Landing.Events
{
	class HotelViewRequestBonusRareEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (Alias.Server.LandingManager.TryGetBonusRare(Constant.BonusRare, out LandingBonusRare bonusRare))
			{
				session.Send(new BonusRareComposer(bonusRare));
			}
		}
	}
}
