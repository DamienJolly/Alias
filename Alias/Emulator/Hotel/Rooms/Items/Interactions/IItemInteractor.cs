using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Items.Interactions
{
	public interface IItemInteractor
	{
		void Serialize(ServerMessage message, RoomItem item);
		void OnUserWalkOn(Session session, Room room, RoomItem item);
		void OnUserWalkOff(Session session, Room room, RoomItem item);
		void OnUserInteract(Session session, Room room, RoomItem item, int state);
		void OnCycle(RoomItem item);
	}
}
