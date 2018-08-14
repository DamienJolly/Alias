using System;
using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Rooms.Trading;
using Alias.Emulator.Hotel.Rooms.Entities.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Rooms.Entities
{
	class RoomEntityManager
	{
		public List<RoomEntity> Entities
		{
			get; set;
		}

		private int VirtualId = 0;

		private Room Room
		{
			get; set;
		}

		public RoomEntityManager(Room room)
		{
			this.Entities = new List<RoomEntity>();
			this.Room = room;
		}

		public void CreateEntity(RoomEntity entity)
		{
			entity.VirtualId = NextVirtualId;
			this.Send(new RoomUsersComposer(entity));
			this.Send(new RoomUserStatusComposer(entity));
			entity.EntityType.OnEntityJoin(entity);
			this.Entities.Add(entity);
		}

		public void OnUserLeave(RoomEntity entity)
		{
			entity.Disposing = true;
			if (!entity.Disposing)
			{
				this.Send(new RoomUserRemoveComposer(entity.VirtualId));
				entity.EntityType.OnEntityLeave(entity);
				entity.Dispose();
				this.Entities.Remove(entity);
			}
		}

		public RoomEntity UserBySession(Session session)
		{
			return this.Entities.Where(user => user.Habbo.Id == session.Habbo.Id).First();
		}

		public RoomEntity UserByVirtualid(int virtualId)
		{
			return this.Entities.Where(user => user.VirtualId == virtualId).First();
		}

		public RoomEntity UserByUserid(int userId)
		{
			return this.Entities.Where(user => user.Habbo.Id == userId).First();
		}

		internal RoomEntity UserByName(string targetname)
		{
			if (this.Entities.Where(user => user.Habbo.Username == targetname).Count() > 0)
			{
				return this.Entities.Where(user => user.Habbo.Username == targetname).First();
			}
			return null;
		}

		public bool UserExists(int virtualId)
		{
			return this.Entities.Where(user => user.VirtualId == virtualId).Count() > 0;
		}

		public void Send(IPacketComposer composer, List<RoomEntity> except)
		{
			ServerPacket message = composer.Compose();
			this.Entities.ForEach(user =>
			{
				if (user.Habbo != null && !except.Contains(user))
				{
					try
					{
						user.Habbo.Session.Send(message, false);
					}
					catch (Exception ex)
					{
						Logging.Error("Couldn't send message to user", ex);
					}
				}
			});
		}

		public void Send(IPacketComposer composer, RoomEntity except)
		{
			this.Entities.ForEach(user =>
			{
				if (user.Habbo != null && user.VirtualId != except.VirtualId)
				{
					try
					{
						user.Habbo.Session.Send(composer, false);
					}
					catch (Exception ex)
					{
						Logging.Error("Couldn't send message to user", ex);
					}
				}
			});
		}

		public void Send(IPacketComposer composer)
		{
			this.Entities.ForEach(user =>
			{
				if (user.Habbo != null)
				{
					try
					{
						user.Habbo.Session.Send(composer, false);
					}
					catch (Exception ex)
					{
						Logging.Error("Couldn't send message to user", ex);
					}
				}
			});
		}

		public void Send(List<IPacketComposer> composers)
		{
			this.Entities.ForEach(user =>
			{
				if (user.Habbo != null)
				{
					try
					{
						user.Habbo.Session.Send(composers, false);
					}
					catch (Exception ex)
					{
						Logging.Error("Couldn't send message to user", ex);
					}
				}
			});
		}

		public int UserCount => this.Entities.Where(entity => entity.Type == RoomEntityType.Player).Count();

		public int NextVirtualId => this.VirtualId++;
	}
}
