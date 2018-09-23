using Alias.Emulator.Hotel.Rooms.Items.Composers;
using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Hotel.Players.Currency.Composers;

namespace Alias.Emulator.Hotel.Rooms.Items.Interactions
{
	class InteractionExchange : IItemInteractor
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

		}

		public void OnUserWalkOff(RoomEntity user, Room room, RoomItem item)
		{

		}

		public void OnUserInteract(RoomEntity user, Room room, RoomItem item, int state)
		{
			if (user.Player.Id != room.RoomData.OwnerId)
			{
				return;
			}

			if (!int.TryParse(item.ItemData.ExtraData, out int amount))
			{
				return;
			}
				
			user.Player.Credits += amount;
			user.Player.Session.Send(new UserCreditsComposer(user.Player));

			room.Mapping.RemoveItem(item);
			room.ItemManager.RemoveItem(item);
			room.EntityManager.Send(new RemoveFloorItemComposer(item));
		}

		public void OnCycle(RoomItem item)
		{

		}
	}
}
