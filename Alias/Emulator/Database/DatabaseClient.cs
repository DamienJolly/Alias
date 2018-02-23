using System;
using System.Data;
using Alias.Emulator.Utilities;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Database
{
	public sealed class DatabaseClient : IDisposable
	{
		public static Credentials Credentials;
		private MySqlConnection Connection;
		private MySqlCommand Command;

		public DatabaseClient()
		{
			this.Connection = new MySqlConnection(DatabaseClient.Credentials.ConnectionString);
			this.Command = this.Connection.CreateCommand();
			this.Connection.Open();
		}

		public static void Initialize()
		{
			DatabaseClient.Credentials = new Credentials(
				Configuration.Value("mysql.username"),
				Configuration.Value("mysql.password"),
				Configuration.Value("mysql.hostname"),
				uint.Parse(Configuration.Value("mysql.port")),
				Configuration.Value("mysql.database"),
				uint.Parse(Configuration.Value("mysql.minsize")),
				uint.Parse(Configuration.Value("mysql.maxsize"))
			);
		}

		public static DatabaseClient Instance()
		{
			return new DatabaseClient();
		}

		public void Dispose()
		{
			this.Connection.Close();
			this.Command.Dispose();
			this.Connection.Dispose();
		}

		public void AddParameter(string sParam, object val)
		{
			this.Command.Parameters.AddWithValue(sParam, val);
		}

		public void Query(string sQuery, int timeout = 30)
		{
			this.Command.CommandTimeout = timeout;
			this.Command.CommandText = sQuery;
			this.Command.ExecuteScalar();
			this.Command.CommandText = null;
		}

		public DataSet DataSet(string Query, int timeout = 30)
		{
			DataSet dataSet = new DataSet();
			this.Command.CommandTimeout = timeout;
			this.Command.CommandText = Query;
			using (MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(this.Command))
			{
				mySqlDataAdapter.Fill(dataSet);
			}
			this.Command.CommandText = null;
			return dataSet;
		}

		public DataTable DataTable(string Query, int timeout = 30)
		{
			DataTable dataTable = new DataTable();
			this.Command.CommandTimeout = timeout;
			this.Command.CommandText = Query;
			using (MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(this.Command))
			{
				mySqlDataAdapter.Fill(dataTable);
			}
			this.Command.CommandText = null;
			return dataTable;
		}

		public DataRow DataRow(string Query, int timeout = 30)
		{
			this.Command.CommandTimeout = timeout;
			DataTable dataTable = this.DataTable(Query);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				return dataTable.Rows[0];
			}
			return null;
		}

		public string String(string Query, int timeout = 30)
		{
			this.Command.CommandTimeout = timeout;
			this.Command.CommandText = Query;
			string result = this.Command.ExecuteScalar().ToString();
			this.Command.CommandText = null;
			return result;
		}

		public string SpecialString(string Query)
		{
			try
			{
				this.Command.CommandTimeout = 30;
				this.Command.CommandText = Query;
				string result = this.Command.ExecuteScalar().ToString();
				this.Command.CommandText = null;
				return result;
			}
			catch { }
			return "";
		}

		public int Int32(string Query, int timeout = 30)
		{
			this.Command.CommandTimeout = timeout;
			this.Command.CommandText = Query;
			int result = int.Parse(this.Command.ExecuteScalar().ToString());
			this.Command.CommandText = null;
			return result;
		}

		public uint UInt32(string Query, int timeout = 30)
		{
			this.Command.CommandTimeout = timeout;
			this.Command.CommandText = Query;
			uint result = (uint)this.Command.ExecuteScalar();
			this.Command.CommandText = null;
			return result;
		}

		public void ClearParameters()
		{
			this.Command.Parameters.Clear();
		}
	}
}
