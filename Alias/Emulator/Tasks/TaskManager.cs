using System;
using System.Threading;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Tasks
{
	public sealed class TaskManager : IDisposable
	{
		private bool IsAlive
		{
			get; set;
		} = true;

		private Thread Cycle
		{
			get; set;
		}

		private Timer sheduler;

		public TaskManager()
		{
			Logging.Title("Alias Emulator - 0 users online - 0 rooms loaded - 0 day(s), 0 hour(s) and 0 minute(s) uptime");
			this.StartCycle();
		}

		private int tick = 0;

		public void OnCycle(object sender)
		{
			tick++;
			if (tick >= 60)
			{
				Logging.Title("Alias Emulator - " + SessionManager.OnlineUsers() + " users online - " + RoomManager.ReadLoadedRooms().Count + " rooms loaded - " + AliasEnvironment.UpTime() + " uptime");
				tick = 0;
			}
			RoomManager.DoRoomCycle();
		}

		public void StartCycle()
		{
			this.Cycle = new Thread(this.OnCycle);
			sheduler = new Timer(new TimerCallback(this.StartPool), null, 500, 500);
		}

		private void StartPool(object sender)
		{
			ThreadPool.UnsafeQueueUserWorkItem(this.OnCycle, null);
		}

		public void StopCycle()
		{
			this.sheduler.Dispose();
		}

		public void Dispose()
		{
			this.sheduler.Dispose();
		}
	}
}
