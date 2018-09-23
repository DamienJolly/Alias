using System.Collections.Generic;
using Alias.Emulator.Hotel.Players.Badges;
using Alias.Emulator.Hotel.Players.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Events
{
	class UserWearBadgeEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			await session.Player.Badges.ResetSlots();

			List<BadgeDefinition> badges = new List<BadgeDefinition>();
			for (int i = 0; i < 5; i++)
			{
				int slot = message.PopInt();
				string code = message.PopString();

				if ((slot < 1 || slot > 5) || code.Length == 0)
				{
					continue;
				}

				if (session.Player.Badges.TryGetBadge(code, out BadgeDefinition badge))
				{
					badge.Slot = slot;
					await session.Player.Badges.UpdateBadgeAsync(badge, code);
				}
			}

			if (session.Player.CurrentRoom != null)
			{
				session.Player.CurrentRoom.EntityManager.Send(new UserBadgesComposer(badges, session.Player.Id));
			}
			else
			{
				session.Send(new UserBadgesComposer(badges, session.Player.Id));
			}
		}
	}
}
