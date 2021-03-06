using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    class ModerationSanctionBanEvent : IPacketEvent
	{
		public const int BAN_18_HOURS = 3;
		public const int BAN_7_DAYS = 4;
		public const int BAN_30_DAYS_STEP_1 = 5;
		public const int BAN_30_DAYS_STEP_2 = 7;
		public const int BAN_100_YEARS = 6;
		public const int BAN_AVATAR_ONLY_100_YEARS = 106;

		public void Handle(Session session, ClientPacket message)
		{
			if (!session.Player.HasPermission("acc_modtool_user_ban"))
			{
				return;
			}
			
			int userId = message.PopInt();
			if (userId <= 0)
			{
				return;
			}

			string reason = message.ToString();
			int topic = message.PopInt();
			int banType = message.PopInt();
			bool unknown = message.PopBoolean();

			int duration = 0;
			switch (banType)
			{
				case BAN_18_HOURS:
					duration = 18 * 60 * 60;
					break;
				case BAN_7_DAYS:
					duration = 7 * 24 * 60 * 60;
					break;
				case BAN_30_DAYS_STEP_1:
				case BAN_30_DAYS_STEP_2:
					duration = 30 * 24 * 60 * 60;
					break;
				case BAN_100_YEARS:
				case BAN_AVATAR_ONLY_100_YEARS:
					duration = (int)UnixTimestamp.Now;
					break;
			}

			Alias.Server.ModerationManager.BanUser(userId, session, reason, duration, ModerationBanType.ACCOUNT, topic);
		}
	}
}
