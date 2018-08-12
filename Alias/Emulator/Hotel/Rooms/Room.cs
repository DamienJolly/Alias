using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Groups.Composers;
using Alias.Emulator.Hotel.Landing.Composers;
using Alias.Emulator.Hotel.Rooms.Items;
using Alias.Emulator.Hotel.Rooms.Items.Tasks;
using Alias.Emulator.Hotel.Rooms.Mapping;
using Alias.Emulator.Hotel.Rooms.Models;
using Alias.Emulator.Hotel.Rooms.Pathfinding;
using Alias.Emulator.Hotel.Rooms.Rights;
using Alias.Emulator.Hotel.Rooms.Tasks;
using Alias.Emulator.Hotel.Rooms.Trading;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Hotel.Rooms.Users.Tasks;
using Alias.Emulator.Network.Packets;

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

		public RoomUserManager UserManager
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

		public Room()
		{

		}

		public void Cycle()
		{
			RoomTask.Start(this);
			if (this.UserManager != null)
			{
				this.UserManager.Users.ForEach(user => user.OnCycle());
				WalkTask.Start(this.UserManager.Users);
			}
			if (this.ItemManager != null)
			{
				// Wired and Effects first
				WiredTask.Start(this.ItemManager.Items.Where(item => item.ItemData.Interaction == ItemInteraction.WIRED_EFFECT).ToList());
				WiredTask.Start(this.ItemManager.Items.Where(item => item.ItemData.Interaction == ItemInteraction.WIRED_TRIGGER).ToList());

				this.RollerTick--;

				ItemTask.Start(this.ItemManager.Items);

				if (this.RollerTick <= 0)
				{
					if (this.RollerMessages.Count > 0)
					{
						this.UserManager.Send(this.RollerMessages);
						this.RollerMessages.Clear();
					}
					this.RollerTick = this.RoomData.RollerSpeed * 2;
				}
			}
		}

		public void Initialize()
		{
			this.PathFinder = new PathFinder(this);
			this.RoomRights = new RoomRights(this);
			this.RoomTrading = new RoomTrading(this);
		}

		public void RefreshGroup()
		{
			if (this.RoomData != null)
			{
				List<RoomUser> users = this.UserManager.Users;
				foreach (RoomUser user in users)
				{
					if (user.Habbo != null)
					{
						user.Habbo.Session.Send(new GroupInfoComposer(this.RoomData.Group, user.Habbo, false, this.RoomData.Group.GetMember(user.Habbo.Id)));
					}
				}
			}
		}

		public void Unload()
		{
			this.Disposing = true;
			List<RoomUser> users = this.UserManager.Users;
			foreach (RoomUser user in users)
			{
				if (user.Habbo != null && user.Habbo.Session != null)
				{
					user.Habbo.CurrentRoom = null;
					user.Habbo.Notification("Sorry, it appears that room has been unloaded!");
					user.Habbo.Session.Send(new HotelViewComposer());
				}
			}
			this.Dispose();
		}

		public void Dispose()
		{
			this.Disposing = true;
			RoomItemDatabase.SaveFurniture(this.ItemManager.Items);
			RoomDatabase.SaveRoom(this.RoomData);
			Alias.Server.RoomManager.RemoveLoadedRoom(this);
		}
	}
}
