using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Items.Interactions
{
	class InteractionTeleport : IItemInteractor
	{
		private int count = 0;

		public void Serialize(ServerPacket message, RoomItem item)
		{
			message.WriteInteger(item.IsLimited ? 256 : 0);
			message.WriteString(item.Mode.ToString());
		}

		public void OnUserEnter(RoomEntity user, RoomItem item)
		{

		}

		public void OnUserLeave(RoomEntity user, RoomItem item)
		{

		}

		public void OnUserWalkOn(RoomEntity user, Room room, RoomItem item)
		{

		}

		public void OnUserWalkOff(RoomEntity user, Room room, RoomItem item)
		{

		}

		public void OnUserInteract(RoomEntity user, Room room, RoomItem item, int state)
		{
			//todo: code me
		}

		public void OnCycle(RoomItem item)
		{

		}
	}
}
