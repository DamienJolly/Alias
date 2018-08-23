using Alias.Emulator.Hotel.Rooms.Items.Composers;
using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Network.Protocol;
using System;
using Alias.Emulator.Hotel.Items;
using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Hotel.Users;

namespace Alias.Emulator.Hotel.Rooms.Items.Interactions
{
	class InteractionCrackable : IItemInteractor
	{
		private bool cracked = false;
		private int count = 0;

		public void Serialize(ServerPacket message, RoomItem item)
		{
			if (!int.TryParse(item.ExtraData, out int count))
			{
				count = 0;
			}

			message.WriteInteger(7 + (item.IsLimited ? 256 : 0));
			message.WriteString((this.cracked ? item.ItemData.Modes - 1 : this.CrackState(count, this.CrackableAmount(item.ItemData.Id), item.ItemData.Modes - 1)) + "");
			message.WriteInteger(count);
			message.WriteInteger(this.CrackableAmount(item.ItemData.Id));
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
			if (!room.RoomRights.HasRights(user.Habbo.Id))
			{
				return;
			}

			if (this.cracked)
			{
				return;
			}
			
			if (!int.TryParse(item.ExtraData, out int count))
			{
				count = 0;
			}
			
			CrackableData data = Alias.Server.ItemManager.GetCrackableData(item.ItemData.Id);
			if (data == null)
			{
				return;
			}

			item.ExtraData = count + 1 + "";
			if (int.Parse(item.ExtraData) >= data.Tick)
			{
				this.cracked = true;
				this.count = 3;
			}

			room.EntityManager.Send(new FloorItemUpdateComposer(item));
		}

		public void OnCycle(RoomItem item)
		{
			if (this.cracked)
			{
				if (this.count <= 0)
				{
					CrackableData data = Alias.Server.ItemManager.GetCrackableData(item.ItemData.Id);
					if (data == null)
					{
						return;
					}

					ItemData itemData = Alias.Server.ItemManager.GetItemData(data.GetRandomReward());
					if (itemData == null)
					{
						return;
					}

					item.ItemData = itemData;
					item.ResetItem(true);
					item.Room.EntityManager.Send(new RemoveFloorItemComposer(item));
					item.Room.EntityManager.Send(new AddFloorItemComposer(item));
					this.cracked = false;
				}
				else
				{
					this.count--;
				}
			}
		}

		private int CrackableAmount(int itemId)
		{
			CrackableData data = Alias.Server.ItemManager.GetCrackableData(itemId);
			return data != null ? data.Tick : 0;
		}

		private int CrackState(int count, int max, int modes) => (int)Math.Floor((1.0D / ((double)max / (double)count) * modes));
	}
}
