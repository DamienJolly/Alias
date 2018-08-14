using Alias.Emulator.Hotel.Rooms.Items.Composers;
using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Items.Interactions
{
	class InteractionDefault : IItemInteractor
	{
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
			System.Console.WriteLine("walk on");
		}

		public void OnUserWalkOff(RoomEntity user, Room room, RoomItem item)
		{
			System.Console.WriteLine("walk off");
		}

		public void OnUserInteract(RoomEntity user, Room room, RoomItem item, int state)
		{
			if (item.ItemData.Modes <= 1 || !room.RoomRights.HasRights(user.Habbo.Id))
			{
				return;
			}

			item.Mode++;
			if (item.Mode >= item.ItemData.Modes)
			{
				item.Mode = 0;
			}

			room.EntityManager.Send(new FloorItemUpdateComposer(item));
		}

		public void OnCycle(RoomItem item)
		{

		}
	}
}
