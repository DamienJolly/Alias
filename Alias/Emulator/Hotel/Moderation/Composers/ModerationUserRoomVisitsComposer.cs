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

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.ModerationUserRoomVisitsMessageComposer);
			message.WriteInteger(this.habbo.Id);
			message.WriteString(this.habbo.Username);
			message.WriteInteger(0); //todo: room visits
			{
				//int - roomid
				//string - roomname
				//int - hours ago
				//int - mins ago
			}
			return message;
		}
	}
}
