using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Moderation.Composers
{
	public class ModerationRoomInfoComposer : IPacketComposer
	{
		private RoomData roomData;

		public ModerationRoomInfoComposer(RoomData roomData)
		{
			this.roomData = roomData;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.ModerationRoomInfoMessageComposer);
			result.Int(this.roomData.Id);
			result.Int(this.roomData.UsersNow);
			result.Boolean(false); //todo: owner in room
			result.Int(this.roomData.OwnerId);
			result.String(this.roomData.OwnerName);
			result.Boolean(false); //todo: public room
			//if () //!= public room
			{
				result.String(this.roomData.Name);
				result.String(this.roomData.Description);
				result.Int(this.roomData.Tags.Count);
				this.roomData.Tags.ForEach(tag =>
				{
					result.String(tag);
				});
			}
			return result;
		}
	}
}
