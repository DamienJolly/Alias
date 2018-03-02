using Alias.Emulator.Hotel.Users.Badges;
using Alias.Emulator.Hotel.Users.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Events
{
	public class RequestWearingBadgesEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			int userId = message.Integer();
			Habbo habbo = SessionManager.SessionById(userId).Habbo;

			if (habbo == null || habbo.Badges == null)
			{
				session.Send(new UserBadgesComposer(BadgeComponent.Initialize(userId), userId));
			}
			else
			{
				session.Send(new UserBadgesComposer(habbo.Badges.GetWearingBadges(), userId));
			}
		}
	}
}