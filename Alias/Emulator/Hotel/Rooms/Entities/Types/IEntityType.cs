using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Entities.Types
{
    interface IEntityType
    {
		void Serialize(ServerPacket message, RoomEntity entity);
		void OnEntityJoin(RoomEntity entity);
		void OnEntityLeave(RoomEntity entity);
		void OnCycle(RoomEntity entity);
	}
}
