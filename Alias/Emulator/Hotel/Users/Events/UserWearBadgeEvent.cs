using System.Collections.Generic;
using Alias.Emulator.Hotel.Users.Badges;
using Alias.Emulator.Hotel.Users.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Events
{
	class UserWearBadgeEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			session.Habbo.Badges.ResetSlots();

			List<BadgeDefinition> badges = new List<BadgeDefinition>();
			for (int i = 0; i < 5; i++)
			{
				int slot = message.PopInt();
				string code = message.PopString();

				if ((slot < 1 || slot > 5) || code.Length == 0)
				{
					continue;
				}

				BadgeDefinition badge = session.Habbo.Badges.GetBadge(code);
				if (badge != null)
				{
					badge.Slot = slot;
					session.Habbo.Badges.UpdateBadge(badge, code);
					badges.Add(badge);
				}
			}

			if (session.Habbo.CurrentRoom != null)
			{
				session.Habbo.CurrentRoom.UserManager.Send(new UserBadgesComposer(badges, session.Habbo.Id));
			}
			else
			{
				session.Send(new UserBadgesComposer(badges, session.Habbo.Id));
			}
		}
	}
}
