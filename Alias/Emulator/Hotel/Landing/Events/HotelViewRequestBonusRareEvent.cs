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
			if (!int.TryParse(Alias.Server.Settings.GetSetting("bonus_rare.id"), out int bonusRareId))
			{
				return;
			}

			if (Alias.Server.LandingManager.TryGetBonusRare(bonusRareId, out LandingBonusRare bonusRare))
			{
				if (!session.Player.BonusRare.TryGetProgress(bonusRareId, out int progress))
				{
					progress = 0;
				}

				session.Send(new BonusRareComposer(bonusRare, progress));
			}
		}
	}
}
