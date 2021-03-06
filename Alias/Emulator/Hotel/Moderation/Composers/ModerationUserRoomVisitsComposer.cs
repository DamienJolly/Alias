using Alias.Emulator.Hotel.Players;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Moderation.Composers
{
	class ModerationUserRoomVisitsComposer : IPacketComposer
	{
		private Player habbo;

		public ModerationUserRoomVisitsComposer(Player habbo)
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
