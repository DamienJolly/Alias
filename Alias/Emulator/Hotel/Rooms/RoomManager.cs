using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Rooms.Models;
using Alias.Emulator.Hotel.Rooms.States;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Rooms
{
	public class RoomManager
	{
		private static List<RoomData> CachedRooms;
		private static List<Room> LoadedRooms;

		public static void Initialize()
		{
			RoomManager.CachedRooms = new List<RoomData>();
			RoomManager.LoadedRooms = new List<Room>();
		}

		public static int TradeToInt(RoomTradeState tradeState)
		{
			switch (tradeState)
			{
				case RoomTradeState.ALLOWED:
					return 1;
				case RoomTradeState.FORBIDDEN:
					return 0;
				default:
					return 0;
			}
		}

		public static RoomTradeState IntToTrade(int state)
		{
			switch (state)
			{
				case 1:
					return RoomTradeState.ALLOWED;
				case 0:
					return RoomTradeState.FORBIDDEN;
				default:
					return RoomTradeState.FORBIDDEN;
			}
		}

		public static RoomDoorState IntToDoor(int state)
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

		public static int DoorToInt(RoomDoorState state)
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

		public static RoomData RoomData(int RoomId)
		{
			if (RoomManager.CachedRooms.Where(room => room.Id == RoomId).ToList().Count > 0)
			{
				return RoomManager.CachedRooms.Where(room => room.Id == RoomId).First();
			}
			return RoomManager.AddToCache(RoomId);
		}

		public static RoomData AddToCache(int RoomId)
		{
			RoomData roomData = RoomDatabase.RoomData(RoomId);
			RoomManager.CachedRooms.Add(roomData);
			return roomData;
		}

		public static List<Room> ReadLoadedRooms()
		{
			return RoomManager.LoadedRooms;
		}

		public static Room LoadRoom(int roomId)
		{
			if (RoomDatabase.RoomExists(roomId))
			{
				Room result = new Room();
				result.Id = roomId;
				result.RoomData = RoomManager.RoomData(roomId);
				result.GameMap = new GameMap(result);
				result.UserManager = new RoomUserManager(result);
				result.Initialize();
				RoomManager.LoadedRooms.Add(result);
				return result;
			}
			else
			{
				Logging.Info("There's no Room with Id " + roomId);
				return new Room();
			}
		}

		public static Room Room(int roomId)
		{
			if (RoomManager.LoadedRooms.Where(r => r.Id == roomId).Count() > 0)
			{
				return RoomManager.LoadedRooms.Where(r => r.Id == roomId).First();
			}
			else
			{
				return RoomManager.LoadRoom(roomId);
			}
		}

		public static void RemoveLoadedRoom(Room room)
		{
			if (RoomManager.LoadedRooms.Where(r => r == room).Count() > 0)
			{
				RoomManager.LoadedRooms.Remove(room);
			}
		}
	}
}
