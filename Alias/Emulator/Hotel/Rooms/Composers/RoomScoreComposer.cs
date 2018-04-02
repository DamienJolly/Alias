using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class RoomScoreComposer : IPacketComposer
	{
		private int Score;
		private bool CanVote;

		public RoomScoreComposer(int s, bool vote)
		{
			this.Score = s;
			this.CanVote = vote;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomScoreMessageComposer);
			message.WriteInteger(this.Score);
			message.WriteBoolean(this.CanVote);
			return message;
		}
	}
}
