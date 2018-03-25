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

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomScoreMessageComposer);
			result.Int(this.Score);
			result.Boolean(this.CanVote);
			return result;
		}
	}
}
