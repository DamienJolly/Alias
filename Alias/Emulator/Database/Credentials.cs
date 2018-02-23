using MySql.Data.MySqlClient;

namespace Alias.Emulator.Database
{
	public class Credentials
	{
		private readonly string Username;
		private readonly string Password;
		private readonly string Hostname;
		private readonly string Database;
		private readonly uint Port;
		private readonly uint MinPoolSize;
		private readonly uint MaxPoolSize;

		public string ConnectionString
		{
			get
			{
				MySqlConnectionStringBuilder ConnString = new MySqlConnectionStringBuilder();
				ConnString.Server = this.Hostname;
				ConnString.Port = this.Port;
				ConnString.UserID = this.Username;
				ConnString.Password = this.Password;
				ConnString.Database = this.Database;
				ConnString.MinimumPoolSize = this.MinPoolSize;
				ConnString.MaximumPoolSize = this.MaxPoolSize;
				ConnString.Pooling = true;
				return ConnString.ToString();
			}
		}

		public Credentials(string username, string password, string hostname, uint port, string database, uint minPoolSize, uint maxPoolSize)
		{
			this.Username = username;
			this.Password = password;
			this.Hostname = hostname;
			this.Port = port;
			this.Database = database;
			this.MinPoolSize = minPoolSize;
			this.MaxPoolSize = maxPoolSize;
		}
	}
}
