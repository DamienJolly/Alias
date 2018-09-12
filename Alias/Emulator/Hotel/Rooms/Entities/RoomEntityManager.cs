using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Rooms.Entities.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

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

		public void LoadAIEntities()
		{
			RoomEntityDatabase.ReadBots(this.Room).ForEach(bot => CreateEntity(bot));
			RoomEntityDatabase.ReadPets(this.Room).ForEach(pet => CreateEntity(pet));
		}

		public void CreateEntity(RoomEntity entity)
		{
			entity.VirtualId = NextVirtualId;
			this.Send(new RoomUsersComposer(entity));
			this.Send(new RoomUserStatusComposer(entity));
			entity.EntityType.OnEntityJoin();
			this.Room.Mapping.Tiles[entity.Position.X, entity.Position.Y].AddEntity(entity);
			this.Entities.Add(entity);
		}
		
		public void OnUserLeave(RoomEntity entity)
		{
			if (!entity.Disposing)
			{
				entity.Disposing = true;
				this.Send(new RoomUserRemoveComposer(entity.VirtualId));
				entity.EntityType.OnEntityLeave();
				entity.Dispose();
				this.Room.Mapping.Tiles[entity.Position.X, entity.Position.Y].RemoveEntity(entity);
				this.Entities.Remove(entity);
			}
		}

		public void Send(List<IPacketComposer> composers, List<RoomEntity> except)
		{
			this.Entities.ForEach(user =>
			{
				if (user.Type == RoomEntityType.Player && !except.Contains(user))
				{
					composers.ForEach(composer =>
					{
						ServerPacket message = composer.Compose();
						user.Habbo.Session.Send(message, false);
					});
				}
			});
		}

		public void Send(IPacketComposer composer) => this.Send(new List<IPacketComposer>() { composer }, new List<RoomEntity>());

		public void Send(List<IPacketComposer> composers) => this.Send(composers, new List<RoomEntity>());

		public void Send(IPacketComposer composer, RoomEntity except) => this.Send(new List<IPacketComposer>() { composer }, new List<RoomEntity>() { except });

		public int UserCount => this.Users.Count;

		public List<RoomEntity> Pets => this.Entities.Where(entity => entity.Type == RoomEntityType.Pet).ToList();

		public List<RoomEntity> Bots => this.Entities.Where(entity => entity.Type == RoomEntityType.Bot).ToList();

		public List<RoomEntity> Users => this.Entities.Where(entity => entity.Type == RoomEntityType.Player).ToList();

		public RoomEntity BotById(int botId) => this.Bots.Where(bot => bot.Id == botId).First();

		public RoomEntity PetById(int petId) => this.Pets.Where(pet => pet.Id == petId).First();

		public RoomEntity EntityByVirtualid(int virtualId) => this.Entities.Where(user => user.VirtualId == virtualId).First();

		public RoomEntity UserByUserid(int userId) => this.Users.Where(user => user.Habbo.Id == userId).First();

		public RoomEntity UserByName(string targetname) => this.Entities.Where(user => user.Name == targetname).First();

		public bool UserExists(int virtualId) => this.Entities.Where(user => user.VirtualId == virtualId).Count() > 0;

		public int NextVirtualId => this.VirtualId++;
	}
}
