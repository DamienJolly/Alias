using System;
using System.Threading;

namespace Alias.Emulator.Tasks
{
	sealed class TaskManager : IDisposable
	{
		private Timer _sheduler;
		private int _tick = 0;

		public TaskManager()
		{
			Console.Title = "Alias Emulator - 0 users online - 0 rooms loaded - 0 day(s), 0 hour(s) and 0 minute(s) uptime";
			this._sheduler = new Timer(new TimerCallback(this.OnCycle), null, 500, 500);
		}
		
		public void OnCycle(object sender)
		{
			this._tick++;
			if (this._tick >= 60)
			{
				TimeSpan Uptime = DateTime.Now - Alias.ServerStarted;
				string uptime = Uptime.Days + " day(s), " + Uptime.Hours + " hour(s) and " + Uptime.Minutes + " minute(s)";
				Console.Title = "Alias Emulator - " + Alias.Server.PlayerManager.OnlinePlayers + " users online - " + Alias.Server.RoomManager.LoadedRooms.Count + " rooms loaded - " + uptime + " uptime";
				this._tick = 0;
			}
			Alias.Server.LandingManager.DoLandingCycle();
			Alias.Server.RoomManager.DoRoomCycle();
			Alias.Server.GroupManager.DoGroupCycle();
		}

		public void Dispose()
		{
			this._sheduler.Dispose();
		}
	}
}
