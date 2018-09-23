using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Players.Composers
{
	class UserClubComposer : IPacketComposer
	{
		private int _totalMinites;
		private int _yearsLeft;
		private int _monthsLeft;
		private int _daysLeft;

		public UserClubComposer(int expireTimestamp)
		{
			int totalDays = (expireTimestamp - (int)UnixTimestamp.Now) / 86400;
			_yearsLeft = totalDays / 365;
			_monthsLeft = (totalDays % 365) / 30;
			_daysLeft = (totalDays % 365) % 30;
			_totalMinites = totalDays * 1440;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserClubMessageComposer);
			message.WriteString("club_habbo");
			message.WriteInteger(_daysLeft);
			message.WriteInteger(1);
			message.WriteInteger(_monthsLeft);
			message.WriteInteger(_yearsLeft);
			message.WriteBoolean(true);
			message.WriteBoolean(true);
			message.WriteInteger(0);
			message.WriteInteger(0);
			message.WriteInteger(_totalMinites);
			return message;
		}
	}
}
