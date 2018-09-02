using System;
using System.Collections.Generic;
using System.IO;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Settings
{
    class SettingsManager
    {
		private Dictionary<string, string> settings;

		public ConfigurationData ConfigData
		{
			get; set;
		}

		public SettingsManager()
		{
			this.settings = new Dictionary<string, string>();
			this.ConfigData = ReadConfig();
		}

		public void Initialize()
		{
			if (this.settings.Count > 0)
			{
				this.settings.Clear();
			}

			this.settings = SettingsDatabase.ReadSettings();
		}

		private ConfigurationData ReadConfig()
		{
			if (!File.Exists(Alias.GetFileFromDictionary(@"configuration.alias")))
			{
				Logging.Exit("Configuration File not found.");
			}
			
			Dictionary<string, string> variables = new Dictionary<string, string>();
			foreach (string line in File.ReadAllLines(Alias.GetFileFromDictionary(@"configuration.alias")))
			{
				if (!line.StartsWith("#") && line.Contains("=") && line.Split('=').Length == 2 && !variables.ContainsKey(line.Split('=')[0]))
				{
					variables.Add(line.Split('=')[0], line.Split('=')[1]);
				}
			}

			ConfigurationData data = new ConfigurationData();
			try
			{
				data.ServerIPAddress = variables["tcp.host"];
				data.ServerPort = int.Parse(variables["tcp.port"]);
				data.ServerMaxConnections = int.Parse(variables["tcp.conlimit"]);
				data.ServerMaxConnectionsPerIP = int.Parse(variables["tcp.conperip"]);
				data.MySQLHostName = variables["mysql.hostname"];
				data.MySQLPort = uint.Parse(variables["mysql.port"]);
				data.MySQLUsername = variables["mysql.username"];
				data.MySQLPassword = variables["mysql.password"];
				data.MySQLDatabase = variables["mysql.database"];
				data.MySQLMinimumPoolSize = uint.Parse(variables["mysql.minsize"]);
				data.MySQLMaximumPoolSize = uint.Parse(variables["mysql.maxsize"]);
			}
			catch
			{
				Logging.Exit("Configuration File was missing some information.");
			}

			return data;
		}

		public string GetSetting(string key)
		{
			if (this.settings.ContainsKey(key))
			{
				return this.settings[key];
			}
			return "";
		}
	}
}
