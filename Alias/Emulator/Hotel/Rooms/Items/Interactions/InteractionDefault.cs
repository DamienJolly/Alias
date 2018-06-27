using Alias.Emulator.Hotel.Rooms.Items.Composers;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Items.Interactions
{
	class InteractionDefault : IItemInteractor
	{
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
			System.Console.WriteLine("walk on");
		}

		public void OnUserWalkOff(Session session, Room room, RoomItem item)
		{
			System.Console.WriteLine("walk off");
		}

		public void OnUserInteract(Session session, Room room, RoomItem item, int state)
		{
			if (item.ItemData.Modes <= 1 || !room.RoomRights.HasRights(session.Habbo.Id))
			{
				return;
			}

			item.Mode++;
			if (item.Mode >= item.ItemData.Modes)
			{
				item.Mode = 0;
			}

			room.UserManager.Send(new FloorItemUpdateComposer(item));
		}

		public void OnCycle(RoomItem item)
		{

		}
	}
}
