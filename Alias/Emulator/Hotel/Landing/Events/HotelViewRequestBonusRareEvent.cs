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
				int progress = session.Habbo.BonusRare.GetProgress(bonusRareId);
				if (progress >= bonusRare.Goal && progress != 0)
				{
					//todo:
					//session.Habbo.BonusRare.GivePrize(bonusRare.Prize);
				}

				session.Send(new BonusRareComposer(bonusRare, progress));
			}
		}
	}
}
