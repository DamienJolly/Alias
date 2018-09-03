using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms.Items;
using Alias.Emulator.Hotel.Rooms.Mapping;
using Alias.Emulator.Hotel.Rooms.Models;
using Alias.Emulator.Hotel.Rooms.Entities;

namespace Alias.Emulator.Hotel.Rooms
{
	sealed class RoomManager
	{
		public RoomModelManager RoomModelManager
		{
			get; set;
		}

		private Dictionary<int, RoomData> CachedRooms
		{
			get; set;
		}

		public Dictionary<int, Room> LoadedRooms
		{
			get; set;
		}

		public RoomManager()
		{
			this.RoomModelManager = new RoomModelManager();
			this.CachedRooms = new Dictionary<int, RoomData>();
			this.LoadedRooms = new Dictionary<int, Room>();
		}

		public void Initialize()
		{
			this.RoomModelManager.Initialize();
		}

		public void DoRoomCycle()
		{
			foreach (Room room in this.LoadedRooms.Values)
			{
				if (room.Disposing)
				{
					continue;
				}

				room.Cycle();
			}
		}

		public bool TryGetRoomData(int roomId, out RoomData roomData)
		{
			roomData = null;
			if (this.CachedRooms.ContainsKey(roomId))
			{
				roomData = this.CachedRooms[roomId];
				return true;
			}
			return TryAddToCache(roomId, out roomData);
		}

		private bool TryAddToCache(int RoomId, out RoomData roomData)
		{
			roomData = RoomDatabase.RoomData(RoomId);
			if (roomData != null)
			{
				this.CachedRooms.Add(RoomId, roomData);
				return true;
			}
			return false;
		}

		public bool TryGetRoom(int roomId, out Room room)
		{
			return this.LoadedRooms.TryGetValue(roomId, out room);
		}

		public Room LoadRoom(int roomId)
		{
			Room room = null;
			if (TryGetRoomData(roomId, out RoomData roomdata))
			{
				room = new Room
				{
					Id = roomId,
					RoomData = roomdata
				};
				room.Model = this.RoomModelManager.GetModel(roomdata.ModelName);
				room.Mapping = new RoomMapping(room);
				room.ItemManager = new RoomItemManager(room);
				room.EntityManager = new RoomEntityManager(room);
				room.Initialize();
				room.Mapping.RegenerateCollisionMap();
				room.EntityManager.LoadAIEntities();
				this.LoadedRooms.Add(roomId, room);
			}
			return room;
		}

		public Room CreateRoom(int ownerId, string name, string description, string modelName, int maxUsers, int tradeType, int categoryId)
		{
			int roomId = RoomDatabase.CreateRoom(ownerId, name, description, modelName, maxUsers, tradeType, categoryId);
			return LoadRoom(roomId);
		}

		public void RemoveLoadedRoom(int roomId)
		{
			if (this.LoadedRooms.ContainsKey(roomId))
			{
				this.LoadedRooms.Remove(roomId);
			}
		}
	}
}
