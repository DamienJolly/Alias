using System;
using System.Threading;
using Alias.Emulator.Hotel.Rooms;

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
			this.StartCycle();
		}

		public void OnCycle(object sender)
		{
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
