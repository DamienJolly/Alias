using Alias.Emulator.Hotel.Rooms.Items.Composers;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;
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

		public void OnUserEnter(RoomUser user, RoomItem item)
		{

		}

		public void OnUserLeave(RoomUser user, RoomItem item)
		{

		}

		public void OnUserWalkOn(Session session, Room room, RoomItem item)
		{
			
		}

		public void OnUserWalkOff(Session session, Room room, RoomItem item)
		{
			
		}

		public void OnUserInteract(Session session, Room room, RoomItem item, int state)
		{
			RoomUser user = room.UserManager.UserBySession(session);
			if (user == null)
			{
				return;
			}

			//if (room.DynamicModel.TilesAdjecent(item.Position.X, item.Position.Y, user.Position.X, user.Position.Y))
			{
				if (item.Mode != -1)
				{
					item.Mode = -1;
					count = 0;
					item.Room.UserManager.Send(new FloorItemUpdateComposer(item));
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
					item.Room.UserManager.Send(new FloorItemUpdateComposer(item));
				}
				count++;
			}
		}
	}
}
