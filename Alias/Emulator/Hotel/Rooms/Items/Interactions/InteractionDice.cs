using Alias.Emulator.Hotel.Rooms.Items.Composers;
using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Rooms.Items.Interactions
{
	class InteractionDice : IItemInteractor
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
			if (room.Mapping.Tiles[item.Position.X, item.Position.Y].TilesAdjecent(room.Mapping.Tiles[user.Position.X, user.Position.Y]))
			{
				if (item.Mode != -1)
				{
					item.Mode = -1;
					count = 0;
					item.Room.EntityManager.Send(new FloorItemUpdateComposer(item));
				}
			}
		}

		public void OnCycle(RoomItem item)
		{
			if (item.Mode == -1)
			{
				if (count >= 2)
				{
					item.Mode = Randomness.RandomNumber(item.ItemData.Modes);
					item.Room.EntityManager.Send(new FloorItemUpdateComposer(item));
				}
				count++;
			}
		}
	}
}
