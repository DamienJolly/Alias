using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Groups.Composers;
using Alias.Emulator.Hotel.Rooms.Items;
using Alias.Emulator.Hotel.Rooms.Items.Tasks;
using Alias.Emulator.Hotel.Rooms.Mapping;
using Alias.Emulator.Hotel.Rooms.Models;
using Alias.Emulator.Hotel.Rooms.Pathfinding;
using Alias.Emulator.Hotel.Rooms.Rights;
using Alias.Emulator.Hotel.Rooms.Trading;
using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Hotel.Groups;

namespace Alias.Emulator.Hotel.Rooms
{
	class Room
	{
		public int Id
		{
			get; set;
		}

		public RoomData RoomData
		{
			get; set;
		}

		public RoomEntityManager EntityManager
		{
			get; set;
		}

		public RoomItemManager ItemManager
		{
			get; set;
		}

		public RoomModel Model
		{
			get; set;
		}

		public RoomMapping Mapping
		{
			get; set;
		}

		public PathFinder PathFinder
		{
			get; set;
		}

		public RoomRights RoomRights
		{
			get; set;
		}

		public RoomTrading RoomTrading
		{
			get; set;
		}

		public List<IPacketComposer> RollerMessages
		{
			get; set;
		} = new List<IPacketComposer>();

		public int IdleTime = 0;
		public int RollerTick = -1;

		public bool Disposing
		{
			get; set;
		} = false;

		public void Cycle()
		{
			this.EntityManager.Entities.Where(entity => !entity.Disposing).ToList().ForEach(entity => entity.OnCycle());

			// Wired and Effects first
			WiredTask.Start(this.ItemManager.Items.Where(item => item.ItemData.Interaction == ItemInteraction.WIRED_EFFECT).ToList());
			WiredTask.Start(this.ItemManager.Items.Where(item => item.ItemData.Interaction == ItemInteraction.WIRED_TRIGGER).ToList());

			this.RollerTick--;
			this.ItemManager.Items.ForEach(item => item.GetInteractor().OnCycle(item));

			if (this.RollerTick <= 0)
			{
				if (this.RollerMessages.Count > 0)
				{
					this.EntityManager.Send(this.RollerMessages);
					this.RollerMessages.Clear();
				}
				this.RollerTick = this.RoomData.RollerSpeed * 2;
			}
		}

		public void Initialize()
		{
			this.PathFinder = new PathFinder(this);
			this.RoomRights = new RoomRights(this);
			this.RoomTrading = new RoomTrading(this);
		}

		public void UpdateGroup(Group group)
		{
			this.RoomData.Group = group;
			List<RoomEntity> users = this.EntityManager.Entities;
			foreach (RoomEntity user in users)
			{
				if (user.Habbo != null)
				{
					user.Habbo.Session.Send(new GroupInfoComposer(group, user.Habbo, false, group.GetMember(user.Habbo.Id)));
				}
			}
		}

		public void Dispose()
		{
			RoomItemDatabase.SaveFurniture(this.ItemManager.Items);
			RoomDatabase.SaveRoom(this.RoomData);
			this.EntityManager.Dispose();
		}
	}
}
