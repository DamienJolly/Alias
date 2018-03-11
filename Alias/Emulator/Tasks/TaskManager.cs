using System.Threading;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Tasks
{
	public class TaskManager
	{
		private static Thread _cycle;
		private static Timer _sheduler;
		private static int _tick = 0;
		
		public static void Initialize()
		{
			Logging.Title("Alias Emulator - 0 users online - 0 rooms loaded - 0 day(s), 0 hour(s) and 0 minute(s) uptime");
			StartCycle();
		}
		
		public static void OnCycle(object sender)
		{
			_tick++;
			if (_tick >= 60)
			{
				Logging.Title("Alias Emulator - " + SessionManager.OnlineUsers() + " users online - " + RoomManager.ReadLoadedRooms().Count + " rooms loaded - " + AliasEnvironment.GetUpTime() + " uptime");
				_tick = 0;
			}
			RoomManager.DoRoomCycle();
		}

		public static void StartCycle()
		{
			_cycle = new Thread(OnCycle);
			_sheduler = new Timer(new TimerCallback(StartPool), null, 500, 500);
		}

		private static void StartPool(object sender)
		{
			ThreadPool.UnsafeQueueUserWorkItem(OnCycle, null);
		}

		public static void StopCycle()
		{
			_sheduler.Dispose();
		}
	}
}
