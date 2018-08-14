using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Entities.Composers
{
	class RoomUserStatusComposer : IPacketComposer
	{
		public List<RoomEntity> users;

		public RoomUserStatusComposer(RoomEntity user)
		{
			this.users = new List<RoomEntity>();
			this.users.Add(user);
		}

		public RoomUserStatusComposer(List<RoomEntity> users)
		{
			this.users = users;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomUserStatusMessageComposer);
			message.WriteInteger(this.users.Count);
			this.users.ForEach(user =>
			{
				message.WriteInteger(user.VirtualId);
				message.WriteInteger(user.Position.X);
				message.WriteInteger(user.Position.Y);
				message.WriteString(user.Position.Z.ToString("0.00"));
				message.WriteInteger(user.Position.HeadRotation);
				message.WriteInteger(user.Position.Rotation);
				message.WriteString(user.Actions.String);
			});
			return message;
		}
	}
}
