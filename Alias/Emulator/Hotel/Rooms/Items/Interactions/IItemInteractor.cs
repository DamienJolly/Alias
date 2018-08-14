using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Items.Interactions
{
	interface IItemInteractor
	{
		void Serialize(ServerPacket message, RoomItem item);
		void OnUserWalkOn(RoomEntity user, Room room, RoomItem item);
		void OnUserWalkOff(RoomEntity user, Room room, RoomItem item);
		void OnUserInteract(RoomEntity user, Room room, RoomItem item, int state);
		void OnCycle(RoomItem item);
	}
}
