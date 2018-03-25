using MySql.Data.MySqlClient;

namespace Alias.Emulator.Database
{
	sealed class Credentials
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Hostname { get; set; }
		public string Database { get; set; }
		public uint Port { get; set; }
		public uint MinPoolSize { get; set; }
		public uint MaxPoolSize { get; set; }

		public string ConnectionString
		{
			get
			{
				MySqlConnectionStringBuilder ConnString = new MySqlConnectionStringBuilder
				{
					Server          = this.Hostname,
					Port            = this.Port,
					UserID          = this.Username,
					Password        = this.Password,
					Database        = this.Database,
					MinimumPoolSize = this.MinPoolSize,
					MaximumPoolSize = this.MaxPoolSize,
					Pooling         = true,
					SslMode         = MySqlSslMode.None
				};
				return ConnString.ToString();
			}
		}
	}
}
