using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms.Composers;
using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Hotel.Rooms.Items.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Hotel.Users.Inventory.Composers;
using Alias.Emulator.Hotel.Rooms.Mapping;

namespace Alias.Emulator.Hotel.Rooms.Items.Events
{
	class RoomPlaceItemEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			if (!room.RoomRights.HasRights(session.Habbo.Id))
			{
				return;
			}

			string[] values = message.PopString().Split(' ');

			if (values.Length < 1)
			{
				Dictionary<string, string> data = new Dictionary<string, string>
				{
					{ "message", "${room.error.cant_set_item}" }
				};
				session.Send(new BubbleAlertComposer("furni_placement_error", data));
				return;
			}

			if (!int.TryParse(values[0], out int itemId))
			{
				return;
			}

			InventoryItem iItem = session.Habbo.Inventory.GetFloorItem(itemId);
			if (iItem == null)
			{
				return;
			}

			{
				if (values.Length < 4)
				{
					return;
				}

				if (!int.TryParse(values[1], out int x)) { return; }
				if (!int.TryParse(values[2], out int y)) { return; }
				if (!int.TryParse(values[3], out int rotation)) { return; }
				
				RoomItem rItem = new RoomItem()
				{
					Id = iItem.Id,
					Room = room,
					ItemData = iItem.ItemData,
					LimitedNumber = iItem.LimitedNumber,
					LimitedStack = iItem.LimitedStack,
					Owner = session.Habbo.Id,
					Position = new ItemPosition()
					{
						X = x,
						Y = y,
						Z = room.Mapping.Tiles[x, y].Position.Z,
						Rotation = rotation
					}
				};
				if (room.Mapping.CanStackAt(x, y, rItem))
				{
					room.ItemManager.AddItem(rItem);
					room.Mapping.AddItem(rItem);
					room.UserManager.Send(new AddFloorItemComposer(rItem));

					iItem.RoomId = room.Id;
					session.Habbo.Inventory.UpdateItem(iItem);
					session.Send(new RemoveHabboItemComposer(iItem.Id));
				}
				else
				{
					Dictionary<string, string> data = new Dictionary<string, string>
					{
						{ "message", "${room.error.cant_set_item}" }
					};
					session.Send(new BubbleAlertComposer("furni_placement_error", data));
					return;
				}
			}
		}
	}
}
