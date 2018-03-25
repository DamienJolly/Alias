using System;
using System.Data;
using Alias.Emulator.Utilities;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Database
{
	/// <summary>
	/// Provides access to an open database connection.
	/// </summary>
	class DatabaseConnection : IDisposable
    {
		private MySqlConnection _connection;
		private MySqlCommand _command;

		DateTime Start;

		public DatabaseConnection(string ConnectionStr)
		{
			this._connection = new MySqlConnection(ConnectionStr);
			this._command = this._connection.CreateCommand();

			if (this._connection.State != ConnectionState.Open)
			{
				this._connection.Open();
			}
			this.Start = DateTime.Now;
		}

		public void AddParameter(string param, object value)
		{
			this._command.Parameters.AddWithValue(param, value);
		}
		
		public void Query(string query, int timeout = 30)
		{
			this._command.CommandTimeout = timeout;
			this._command.CommandText = query;

			try
			{
				this._command.ExecuteNonQuery();
			}
			catch (MySqlException e)
			{
				Logging.Error("MySql Error: ", e);
				throw e;
			}
			finally
			{
				this._command.CommandText = string.Empty;
				this._command.Parameters.Clear();
			}
		}

		public MySqlDataReader DataReader(string Query, int timeout = 30)
		{
			this._command.CommandTimeout = timeout;
			this._command.CommandText = Query;

			try
			{
				return this._command.ExecuteReader();
			}
			catch (MySqlException e)
			{
				Logging.Error("MySql Error: ", e);
				throw e;
			}
			finally
			{
				this._command.CommandText = string.Empty;
				this._command.Parameters.Clear();
			}
		}

		public int LastInsertedId()
		{
			try
			{
				return (int)this._command.LastInsertedId;
			}
			catch (MySqlException e)
			{
				Logging.Error("MySql Error: ", e);
				throw e;
			}
			finally
			{
				this._command.CommandText = string.Empty;
			}
		}

		public void Dispose()
		{
			if (this._connection.State == ConnectionState.Open)
			{
				this._connection.Close();
				this._connection = null;
			}

			if (this._command != null)
			{
				this._command.Dispose();
				this._command = null;
			}

			int Finish = (DateTime.Now - Start).Milliseconds;
			
			if (Finish >= 5000)
			{
				Logging.Warn("Query took 5 seconds or longer");
			}
		}
	}
}
