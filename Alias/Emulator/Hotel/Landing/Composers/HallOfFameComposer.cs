using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Landing.Composers
{
	public class HallOfFameComposer : IPacketComposer
	{
		private LandingCompetition competition;

		public HallOfFameComposer(LandingCompetition competition)
		{
			this.competition = competition;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.HallOfFameMessageComposer);
			message.WriteString(this.competition.Name);
			message.WriteInteger(this.competition.Users.Count);

			int count = 1;
			this.competition.Users.ForEach(user =>
			{
				message.WriteInteger(user.Id);
				message.WriteString(user.Username);
				message.WriteString(user.Figure);
				message.WriteInteger(count);
				message.WriteInteger(user.Points);
				count++;
			});
			return message;
		}
	}
}
