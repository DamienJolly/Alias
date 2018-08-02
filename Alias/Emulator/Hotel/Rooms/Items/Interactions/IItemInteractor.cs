using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Items.Interactions
{
	interface IItemInteractor
	{
		void Serialize(ServerPacket message, RoomItem item);
		void OnUserWalkOn(RoomUser user, Room room, RoomItem item);
		void OnUserWalkOff(RoomUser user, Room room, RoomItem item);
		void OnUserInteract(RoomUser user, Room room, RoomItem item, int state);
		void OnCycle(RoomItem item);
	}
}
