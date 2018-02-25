using System;
using System.Collections.Generic;
using System.Threading;
using Alias.Emulator.Hotel.Rooms.Cycle.Tasks;
using Alias.Emulator.Hotel.Rooms.Users;

namespace Alias.Emulator.Hotel.Rooms.Cycle
{
	public sealed class RoomCycleTask : IDisposable
	{
		public Room Room
		{
			get; set;
		}

		private bool IsAlive
		{
			get; set;
		} = true;

		private Thread Cycle
		{
			get; set;
		}
		
		private Timer sheduler;

		public void OnCycle(object sender)
		{
			WalkTask.Start(new List<RoomUser>(this.Room.UserManager.Users));
			RoomTask.Start(this.Room);
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
