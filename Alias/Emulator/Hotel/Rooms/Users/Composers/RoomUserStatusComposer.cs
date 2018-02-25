using System.Collections.Generic;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Users.Composers
{
	public class RoomUserStatusComposer : MessageComposer
	{
		public List<RoomUser> users;

		public RoomUserStatusComposer(RoomUser user)
		{
			this.users = new List<RoomUser>();
			this.users.Add(user);
		}

		public RoomUserStatusComposer(List<RoomUser> users)
		{
			this.users = users;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomUserStatusMessageComposer);
			result.Int(this.users.Count);
			this.users.ForEach(user =>
			{
				result.Int(user.VirtualId);
				result.Int(user.Position.X);
				result.Int(user.Position.Y);
				result.String(user.Position.Z.ToString("0.00"));
				result.Int(user.Position.HeadRotation);
				result.Int(user.Position.Rotation);
				result.String(user.Actions.String);
			});
			return result;
		}
	}
}
