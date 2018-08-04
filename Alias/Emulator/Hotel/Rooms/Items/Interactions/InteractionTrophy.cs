using Alias.Emulator.Hotel.Rooms.Users;
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

		public void OnUserEnter(RoomUser user, RoomItem item)
		{

		}

		public void OnUserLeave(RoomUser user, RoomItem item)
		{

		}

		public void OnUserWalkOn(RoomUser user, Room room, RoomItem item)
		{

		}

		public void OnUserWalkOff(RoomUser user, Room room, RoomItem item)
		{

		}

		public void OnUserInteract(RoomUser user, Room room, RoomItem item, int state)
		{

		}

		public void OnCycle(RoomItem item)
		{

		}
	}
}
