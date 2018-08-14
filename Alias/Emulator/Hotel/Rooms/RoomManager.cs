using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Rooms.Items;
using Alias.Emulator.Hotel.Rooms.Mapping;
using Alias.Emulator.Hotel.Rooms.Models;
using Alias.Emulator.Hotel.Rooms.States;
using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Rooms
{
	sealed class RoomManager
	{
		public RoomModelManager RoomModelManager
		{
			get; set;
		}

		private List<RoomData> _cachedRooms;
		private List<Room> _loadedRooms;

		public RoomManager()
		{
			this.RoomModelManager = new RoomModelManager();
			this._cachedRooms = new List<RoomData>();
			this._loadedRooms = new List<Room>();
		}

		public void Initialize()
		{
			this.RoomModelManager.Initialize();
		}

		public int TradeToInt(RoomTradeState tradeState)
		{
			switch (tradeState)
			{
				case RoomTradeState.ALLOWED:
					return 2;
				case RoomTradeState.OWNER:
					return 1;
				case RoomTradeState.FORBIDDEN:
					return 0;
				default:
					return 0;
			}
		}

		public RoomTradeState IntToTrade(int state)
		{
			switch (state)
			{
				case 2:
					return RoomTradeState.ALLOWED;
				case 1:
					return RoomTradeState.OWNER;
				case 0:
					return RoomTradeState.FORBIDDEN;
				default:
					return RoomTradeState.FORBIDDEN;
			}
		}

		public RoomDoorState IntToDoor(int state)
		{
			switch (state)
			{
				case 0:
					return RoomDoorState.OPEN;
				case 1:
					return RoomDoorState.CLOSED;
				case 2:
					return RoomDoorState.PASSWORD;
				default:
					return RoomDoorState.OPEN;
			}
		}

		public int DoorToInt(RoomDoorState state)
		{
			switch (state)
			{
				case RoomDoorState.OPEN:
					return 0;
				case RoomDoorState.CLOSED:
					return 1;
				case RoomDoorState.PASSWORD:
					return 2;
				default:
					return 0;
			}
		}

		public RoomData RoomData(int RoomId)
		{
			if (this._cachedRooms.Where(room => room.Id == RoomId).ToList().Count > 0)
			{
				return this._cachedRooms.Where(room => room.Id == RoomId).First();
			}
			return AddToCache(RoomId);
		}

		public RoomData AddToCache(int RoomId)
		{
			RoomData roomData = RoomDatabase.RoomData(RoomId);
			this._cachedRooms.Add(roomData);
			return roomData;
		}

		public void DoRoomCycle()
		{
			this._loadedRooms.Where(room => !room.Disposing).ToList().ForEach(room => room.Cycle());
		}

		public List<Room> ReadLoadedRooms()
		{
			return this._loadedRooms;
		}

		public Room LoadRoom(int roomId)
		{
			if (RoomDatabase.RoomExists(roomId))
			{
				Room result = new Room()
				{
					Id = roomId,
					RoomData = RoomData(roomId)
				};
				result.Model = this.RoomModelManager.GetModel(result.RoomData.ModelName);
				result.Mapping = new RoomMapping(result);
				result.ItemManager = new RoomItemManager(result);
				result.EntityManager = new RoomEntityManager(result);
				result.Initialize();
				result.Mapping.RegenerateCollisionMap();
				this._loadedRooms.Add(result);
				return result;
			}
			else
			{
				Logging.Info("There's no Room with Id " + roomId);
				return new Room();
			}
		}

		public Room CreateRoom(int ownerId, string name, string description, string modelName, int maxUsers, int tradeType, int categoryId)
		{
			int roomId = RoomDatabase.CreateRoom(ownerId, name, description, modelName, maxUsers, tradeType, categoryId);
			return LoadRoom(roomId);
		}

		public Room Room(int roomId)
		{
			if (IsRoomLoaded(roomId))
			{
				return this._loadedRooms.Where(r => r.Id == roomId).First();
			}
			else
			{
				return LoadRoom(roomId);
			}
		}

		public bool IsRoomLoaded(int roomId)
		{
			return this._loadedRooms.Where(r => r.Id == roomId).Count() > 0;
		}

		public void RemoveLoadedRoom(Room room)
		{
			if (IsRoomLoaded(room.Id))
			{
				this._loadedRooms.Remove(room);
			}
		}
	}
}
