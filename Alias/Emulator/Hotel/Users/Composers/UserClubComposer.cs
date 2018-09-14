using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Users.Composers
{
	class UserClubComposer : IPacketComposer
	{
		private int totalMinites;
		private int yearsLeft;
		private int monthsLeft;
		private int daysLeft;

		public UserClubComposer(int expireTimestamp)
		{
			int totalDays = (expireTimestamp - (int)UnixTimestamp.Now) / 86400;
			this.yearsLeft = totalDays / 365;
			this.monthsLeft = (totalDays % 365) / 30;
			this.daysLeft = (totalDays % 365) % 30;
			this.totalMinites = totalDays * 1440;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserClubMessageComposer);
			message.WriteString("club_habbo");
			message.WriteInteger(this.daysLeft);
			message.WriteInteger(1);
			message.WriteInteger(this.monthsLeft);
			message.WriteInteger(this.yearsLeft);
			message.WriteBoolean(true);
			message.WriteBoolean(true);
			message.WriteInteger(0);
			message.WriteInteger(0);
			message.WriteInteger(this.totalMinites);
			return message;
		}
	}
}
