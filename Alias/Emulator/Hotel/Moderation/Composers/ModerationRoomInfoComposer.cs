using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Moderation.Composers
{
	class ModerationRoomInfoComposer : IPacketComposer
	{
		private RoomData roomData;

		public ModerationRoomInfoComposer(RoomData roomData)
		{
			this.roomData = roomData;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.ModerationRoomInfoMessageComposer);
			message.WriteInteger(this.roomData.Id);
			message.WriteInteger(this.roomData.UsersNow);
			message.WriteBoolean(Alias.Server.PlayerManager.IsOnline(this.roomData.OwnerId));
			message.WriteInteger(this.roomData.OwnerId);
			message.WriteString(this.roomData.OwnerName);
			message.WriteBoolean(false); //todo: public room
			//if () //!= public room
			{
				message.WriteString(this.roomData.Name);
				message.WriteString(this.roomData.Description);
				message.WriteInteger(this.roomData.Tags.Count);
				this.roomData.Tags.ForEach(tag =>
				{
					message.WriteString(tag);
				});
			}
			return message;
		}
	}
}
