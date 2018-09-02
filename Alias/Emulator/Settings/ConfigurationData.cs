namespace Alias.Emulator.Settings
{
	class ConfigurationData
	{
		public string ServerIPAddress
		{
			get; set;
		}

		public int ServerPort
		{
			get; set;
		}

		public int ServerMaxConnections
		{
			get; set;
		}

		public int ServerMaxConnectionsPerIP
		{
			get; set;
		}

		public string MySQLHostName
		{
			get; set;
		}

		public uint MySQLPort
		{
			get; set;
		}

		public string MySQLUsername
		{
			get; set;
		}

		public string MySQLPassword
		{
			get; set;
		}

		public string MySQLDatabase
		{
			get; set;
		}

		public uint MySQLMinimumPoolSize
		{
			get; set;
		}

		public uint MySQLMaximumPoolSize
		{
			get; set;
		}
	}
}
