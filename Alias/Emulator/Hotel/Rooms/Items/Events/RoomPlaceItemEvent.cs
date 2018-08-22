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
			if (values.Length < 4)
			{
				Dictionary<string, string> data = new Dictionary<string, string>
				{
					{ "message", "${room.error.cant_set_item}" }
				};
				session.Send(new BubbleAlertComposer("furni_placement_error", data));
				return;
			}

			if (!int.TryParse(values[0], out int itemId)) { return; }

			InventoryItem iItem = session.Habbo.Inventory.GetFloorItem(itemId);
			if (iItem == null)
			{
				return;
			}

			RoomItem rItem = new RoomItem()
			{
				Id = iItem.Id,
				Room = room,
				ItemData = iItem.ItemData,
				LimitedNumber = iItem.LimitedNumber,
				LimitedStack = iItem.LimitedStack,
				Owner = session.Habbo.Id,
				ExtraData = iItem.ExtraData,
				Mode = iItem.Mode,
				Position = new ItemPosition()
			};

			if (iItem.ItemData.Type == "s")
			{
				if (values.Length < 4)
				{
					return;
				}

				if (int.TryParse(values[1], out int x))
				{
					rItem.Position.X = x;
				}
				if (int.TryParse(values[2], out int y))
				{
					rItem.Position.Y = y;
				}
				if (int.TryParse(values[3], out int rotation))
				{
					rItem.Position.Rotation = rotation;
				}
				rItem.Position.Z = room.Mapping.Tiles[rItem.Position.X, rItem.Position.Y].Height;

				if (!room.Mapping.CanStackAt(rItem.Position.X, rItem.Position.Y, rItem))
				{
					Dictionary<string, string> data = new Dictionary<string, string>
					{
						{ "message", "${room.error.cant_set_item}" }
					};
					session.Send(new BubbleAlertComposer("furni_placement_error", data));
					return;
				}

				room.Mapping.AddItem(rItem);
				room.EntityManager.Send(new AddFloorItemComposer(rItem));
			}
			else
			{
				rItem.Position.WallPosition = values[1] + " " + values[2] + " " + values[3];
				session.Send(new AddWallItemComposer(rItem));
			}

			room.ItemManager.AddItem(rItem);
			iItem.RoomId = room.Id;
			session.Habbo.Inventory.UpdateItem(iItem);
			session.Send(new RemoveHabboItemComposer(iItem.Id));
		}
	}
}
