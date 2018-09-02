using MySql.Data.MySqlClient;

namespace Alias.Emulator.Database
{
	sealed class DatabaseManager
	{
		private readonly string _conStr;

		public DatabaseManager(string connectionStr)
		{
			this._conStr = connectionStr;
		}

		public bool TestConnection()
		{
			try
			{
				using (DatabaseConnection client = GetConnection())
				{
					client.Query("SELECT 1+1;");
				}
			}
			catch (MySqlException)
			{
				return false;
			}

			return true;
		}

		public DatabaseConnection GetConnection()
		{
			return new DatabaseConnection(this._conStr);
		}
	}
}
