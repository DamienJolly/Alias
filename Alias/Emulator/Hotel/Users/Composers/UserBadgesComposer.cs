using System.Collections.Generic;
using Alias.Emulator.Hotel.Users.Badges;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Composers
{
	public class UserBadgesComposer : IMessageComposer
	{
		private List<BadgeDefinition> badges;
		private int id;

		public UserBadgesComposer(List<BadgeDefinition> badges, int id)
		{
			this.badges = badges;
			this.id = id;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.UserBadgesMessageComposer);
			message.Int(this.id);
			message.Int(badges.Count);
			badges.ForEach(badge =>
			{
				message.Int(badge.Slot);
				message.String(badge.Code);
			});
			return message;
		}
	}
}
