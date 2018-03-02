using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms.Composers;
using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Hotel.Rooms.Items.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Hotel.Users.Inventory.Composers;

namespace Alias.Emulator.Hotel.Rooms.Items.Events
{
	public class RoomPlaceItemEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
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

			string[] values = message.String().Split(' ');

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

				if (room.DynamicModel.CanStackAt(x, y))
				{
					RoomItem rItem = new RoomItem()
					{
						Id = iItem.Id,
						Room = room,
						ItemData = iItem.ItemData,
						Owner = session.Habbo.Id,
						Position = new ItemPosition()
						{
							X = x,
							Y = y,
							Z = room.DynamicModel.GetTileHeight(x, y),
							Rotation = rotation
						}
					};
					room.ItemManager.AddItem(rItem);
					room.UserManager.Send(new AddFloorItemComposer(rItem));

					session.Habbo.Inventory.RemoveItem(iItem);
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