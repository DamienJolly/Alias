using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Moderation.Composers
{
	public class ModerationUserRoomVisitsComposer : IPacketComposer
	{
		private Habbo habbo;

		public ModerationUserRoomVisitsComposer(Habbo habbo)
		{
			this.habbo = habbo;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.ModerationUserRoomVisitsMessageComposer);
			result.Int(this.habbo.Id);
			result.String(this.habbo.Username);
			result.Int(0); //todo: room visits
			{
				//int - roomid
				//string - roomname
				//int - hours ago
				//int - mins ago
			}
			return result;
		}
	}
}
