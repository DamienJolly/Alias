using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Entities.Composers
{
	class RoomUsersComposer : IPacketComposer
	{
		private List<RoomEntity> users;

		public RoomUsersComposer(RoomEntity user)
		{
			this.users = new List<RoomEntity>{ user };
		}

		public RoomUsersComposer(List<RoomEntity> users)
		{
			this.users = users;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomUsersMessageComposer);
			message.WriteInteger(this.users.Count);
			this.users.ForEach(user =>
			{
				message.WriteInteger(user.Id);
				message.WriteString(user.Name);
				message.WriteString(user.Motto);
				message.WriteString(user.Look);
				message.WriteInteger(user.VirtualId);
				message.WriteInteger(user.Position.X);
				message.WriteInteger(user.Position.Y);
				message.WriteString(user.Position.Z.ToString());
				message.WriteInteger(user.Position.Rotation);
				user.EntityType.Serialize(message, user);
			});
			return message;
		}
	}
}
