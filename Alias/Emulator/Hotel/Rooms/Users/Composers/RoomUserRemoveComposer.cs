using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Users.Composers
{
	public class RoomUserRemoveComposer : IPacketComposer
	{
		private int VirtualId;

		public RoomUserRemoveComposer(int virtualId)
		{
			this.VirtualId = virtualId;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomUserRemoveMessageComposer);
			result.String(this.VirtualId.ToString());
			return result;
		}
	}
}
