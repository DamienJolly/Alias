using System;
using System.IO;
using System.Collections.Generic;

namespace Alias.Emulator.Utilities
{
	public class Configuration
	{
		private static Dictionary<string, string> Variables;

		public static void Initialize()
		{
			Configuration.Variables = new Dictionary<string, string>();
			if (!File.Exists(Constant.ConfigurationFile))
			{
				Logging.Error("Configuration File not found. ", new Exception(), "ConfigurationFile", "Initialize");
				Logging.Info("Press any key to exit.");
				Logging.ReadLine();
				Environment.Exit(0);
			}

			foreach (string line in File.ReadAllLines(Constant.ConfigurationFile))
			{
				if (!line.StartsWith("#") && line.Contains("=") && line.Split('=').Length == 2 && !Configuration.Variables.ContainsKey(line.Split('=')[0]))
				{
					Configuration.Variables.Add(line.Split('=')[0], line.Split('=')[1]);
				}
			}
		}

		public static string Value(string key)
		{
			if (Configuration.Variables.ContainsKey(key))
			{
				return Configuration.Variables[key];
			}
			return "";
		}
	}
}
