using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Entities.Types
{
    abstract class IEntityType
    {
		public RoomEntity Entity
		{
			get; set;
		}

		public Room CurrentRoom
		{
			get; set;
		}

		public abstract void Serialize(ServerPacket message);
		public abstract void OnEntityJoin();
		public abstract void OnEntityLeave();
		public abstract void OnCycle();
	}
}
