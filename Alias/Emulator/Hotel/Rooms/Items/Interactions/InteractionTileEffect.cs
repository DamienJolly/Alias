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
	class InteractionTileEffect : IItemInteractor
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
			user.SetEffectId(GetRandomEffectId(item));
		}

		public void OnUserWalkOff(RoomUser user, Room room, RoomItem item)
		{
			user.SetEffectId(0);
		}

		public void OnUserInteract(RoomUser user, Room room, RoomItem item, int state)
		{

		}

		public void OnCycle(RoomItem item)
		{
			if (item.Room.Mapping.Tiles[item.Position.X, item.Position.Y].Entities.Count != 0)
			{
				if (item.Mode != 1)
				{
					item.Mode = 1;
					item.Room.UserManager.Send(new FloorItemUpdateComposer(item));
				}
				count = 0;
			}
			else
			{
				if (item.Mode != 0 && count >= 2)
				{
					item.Mode = 0;
					item.Room.UserManager.Send(new FloorItemUpdateComposer(item));
				}
				count++;
			}
		}
		
		private int GetRandomEffectId(RoomItem item)
		{
			int id = 0;
			List<int> items = item.ItemData.ExtraData.Split(',').Select(Int32.Parse).ToList();
			if (items.Count != 0)
			{
				id = items[Randomness.RandomNumber(items.Count) - 1];
			}
			return id;
		}
	}
}
