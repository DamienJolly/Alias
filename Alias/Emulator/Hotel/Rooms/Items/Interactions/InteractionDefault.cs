using Alias.Emulator.Hotel.Rooms.Items.Composers;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Items.Interactions
{
	public class InteractionDefault : IItemInteractor
	{
		public void Serialize(ServerMessage message, RoomItem item)
		{
			message.Int(0); //(item.LimitedNo > 0 ? 256 : 0);
			message.String(item.Mode.ToString());
		}

		public void OnUserEnter(RoomUser user, RoomItem item)
		{

		}

		public void OnUserLeave(RoomUser user, RoomItem item)
		{

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
