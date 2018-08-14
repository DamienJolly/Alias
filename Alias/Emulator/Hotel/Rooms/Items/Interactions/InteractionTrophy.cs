using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Items.Interactions
{
	class InteractionTrophy : IItemInteractor
	{
		public void Serialize(ServerPacket message, RoomItem item)
		{
			message.WriteInteger(item.IsLimited ? 256 : 0);
			message.WriteString(item.ExtraData);
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

		}

		public void OnCycle(RoomItem item)
		{

		}
	}
}
