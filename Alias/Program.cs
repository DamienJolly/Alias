using System;
using System.Diagnostics;
using System.IO;
using System.Runtime;
using Alias.Emulator.Utilities;

namespace Alias
{
    static class Program
    {
		/// <summary>
		/// Entry point for the application
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			Logging.Alias("Alias Emulator is starting up...", Emulator.Alias.Version);
			Logging.CreateExceptionFile();

			if (!GCSettings.IsServerGC)
			{
				Logging.Warn("GC is not configured to server mode.");
			}

			Logging.Debug("GC latency mode is set to " + GCSettings.LatencyMode + " with GC Server set to " + (GCSettings.IsServerGC ? "Enabled" : "Disabled"));

			Stopwatch sw = new Stopwatch();
			sw.Start();

			bool Running64Bit = (IntPtr.Size == 8);

			if (!Running64Bit)
			{
				Logging.Warn("This application is not running in 64-bit, we recommend you run it in 64-bit. Press any key to continue..");
				Console.ReadKey();
			}

			Logging.Info("Loading configuration file.");
			Configuration.Initialize();

			Logging.Info("Starting to initialize Server.");

			Emulator.Alias BaseMango = new Emulator.Alias(args);
			GC.KeepAlive(BaseMango);

			sw.Stop();
			Logging.Debug("Time taken to start: " + sw.Elapsed.TotalSeconds.ToString().Split('.')[0] + " seconds.");

			while (true)
			{
				if (Console.ReadKey(true).Key == ConsoleKey.Enter)
				{
					//todo: redo console commands
				}
			}
		}
	}
}
