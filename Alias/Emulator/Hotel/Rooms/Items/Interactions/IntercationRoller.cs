using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms.Items.Composers;
using Alias.Emulator.Hotel.Rooms.Mapping;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Hotel.Rooms.Users.Composers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Items.Interactions
{
	class InteractionRoller : IItemInteractor
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
			Room room = item.Room;
			if (room.RollerTick == 0)
			{
				RoomTile oldTile = room.Mapping.Tiles[item.Position.X, item.Position.Y];
				RoomTile newTile = room.Mapping.GetTileInFront(oldTile, item.Position.Rotation);

				if (newTile == null || newTile.State == RoomTileState.CLOSED || newTile.Entities.Count > 0)
				{
					return;
				}
				
				List<RoomUser> habbosOnRoller = oldTile.Entities;
				List<RoomItem> itemsOnRoller = oldTile.Items;
				List<RoomItem> itemsNewTile = newTile.Items;
				RoomItem newRoller = null;
				RoomItem topItem = newTile.TopItem;

				bool allowUsers = true;
				bool allowFurniture = true;
				bool stackContainsRoller = false;
				foreach (RoomItem rItem in itemsNewTile)
				{
					if (!(rItem.ItemData.CanWalk || rItem.ItemData.CanSit))
                    {
						allowUsers = false;
					}
					if (rItem.ItemData.Interaction == ItemInteraction.ROLLER)
                    {
						newRoller = item;
						stackContainsRoller = true;

						if (itemsNewTile.Count > 1 && item != topItem)
						{
							allowUsers = false;
							allowFurniture = false;
							continue;
						}

						break;
					}
                    else
					{
						allowFurniture = false;
					}
				}

				double zOffset = 0;
				if (newRoller == null)
				{
					zOffset = -item.ItemData.Height;
				}

				if (allowFurniture || (!allowFurniture && !stackContainsRoller))
				{
					foreach (RoomItem rItem in itemsOnRoller)
					{
						if (rItem != item)
						{
							room.RollerMessages.Add(new FloorItemOnRollerComposer(rItem, item, oldTile, newTile, zOffset));
						}
					}
				}

				if (allowUsers)
				{
					foreach (RoomUser user in habbosOnRoller)
					{
						if (stackContainsRoller && !allowFurniture && !(topItem != null && topItem.ItemData.CanWalk))
						{
							continue;
						}

						if (!user.Actions.Has("mv"))
						{
							if (user.Actions.Has("sit"))
							{
								user.Actions.Remove("sit");
							}
							if (user.Actions.Has("lay"))
							{
								user.Actions.Remove("lay");
							}

							room.RollerMessages.Add(new RoomUserOnRollerComposer(user, item, oldTile, newTile, zOffset));
						}
					}
				}
			}
		}
	}
}
