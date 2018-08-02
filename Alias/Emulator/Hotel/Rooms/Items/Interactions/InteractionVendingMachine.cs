using System;
using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Rooms.Items.Composers;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Hotel.Rooms.Users.Composers;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Rooms.Items.Interactions
{
	class InteractionVendingMachine : IItemInteractor
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
			if (room.Mapping.Tiles[item.Position.X, item.Position.Y].TilesAdjecent(room.Mapping.Tiles[user.Position.X, user.Position.Y]))
			{
				if (item.Mode == 1)
				{
					return;
				}

				if (!user.Actions.Has("sit"))
				{
					user.Position.CalculateRotation(item.Position.X, item.Position.Y);
					user.Actions.Remove("mv");
					room.UserManager.Send(new RoomUserStatusComposer(user));
				}
				item.Mode = 1;
				item.InteractingUser = user;
				count = 0;
				room.UserManager.Send(new FloorItemUpdateComposer(item));
			}
		}

		public void OnCycle(RoomItem item)
		{
			if (item.Mode == 1)
			{
				if (count >= 1)
				{
					item.InteractingUser.HandItem = GetRandomVendingMachineId(item);
					item.Room.UserManager.Send(new RoomUserHandItemComposer(item.InteractingUser));
					item.Mode = 0;
					item.Room.UserManager.Send(new FloorItemUpdateComposer(item));
				}
				count++;
			}
		}

		private int GetRandomVendingMachineId(RoomItem item)
		{
			int id = 0;
			List<int> items = item.ItemData.ExtraData.Split(',').Select(Int32.Parse).ToList();
			if (items.Count != 0)
			{
				id = items[Randomness.RandomNumber(items.Count)];
			}
			return id;
		}
	}
}
