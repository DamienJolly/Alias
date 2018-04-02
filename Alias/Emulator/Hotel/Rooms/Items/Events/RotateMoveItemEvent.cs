using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms.Composers;
using Alias.Emulator.Hotel.Rooms.Items.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Items.Events
{
	public class RotateMoveItemEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			RoomItem item = room.ItemManager.GetItem(message.PopInt());
			if (item == null)
			{
				return;
			}

			if (!room.RoomRights.HasRights(session.Habbo.Id))
			{
				return;
			}

			int x = message.PopInt();
			int y = message.PopInt();

			if (room.DynamicModel.CanStackAt(x, y, item))
			{
				double height = room.DynamicModel.GetTileHeight(x, y);
				if (item == room.DynamicModel.GetTopItemAt(x, y))
				{
					height = height - item.ItemData.Height;
				}
				item.Position.Z = height;
				item.Position.X = x;
				item.Position.Y = y;
				item.Position.Rotation = message.PopInt();
			}
			else
			{
				Dictionary<string, string> data = new Dictionary<string, string>
				{
					{ "message", "${room.error.cant_set_item}" }
				};
				session.Send(new BubbleAlertComposer("furni_placement_error", data));
			}


			room.UserManager.Send(new FloorItemUpdateComposer(item));
		}
	}
}
