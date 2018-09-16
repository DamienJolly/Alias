using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace Alias.Emulator.Database
{
	public abstract class AbstractDao
	{
		public static string ConnectionString { get; set; }

		private async Task OpenConnection(Action<MySqlConnection> conAction)
		{
			using (MySqlConnection connection = new MySqlConnection(ConnectionString))
			{
				await connection.OpenAsync();
				conAction(connection);
				await connection.CloseAsync();
			}
		}

		protected async Task StartTransactionAsync(Action<MySqlTransaction> transaction)
		{
			await OpenConnection(async connection =>
			{
				using (MySqlTransaction tran = await connection.BeginTransactionAsync())
				{
					transaction(tran);
					tran.Commit();
				}
			});
		}

		protected async Task SelectAsync(Action<DbDataReader> reader, string query, params object[] parameters)
		{
			try
			{
				await OpenConnection(async connection =>
				{
					using (MySqlCommand command = connection.CreateCommand())
					{
						command.CommandText = query;
						SetupParameters(command.Parameters, parameters);

						using (DbDataReader dbReader = await command.ExecuteReaderAsync())
						{
							reader(dbReader);
						}
					}
				});
			}
			catch (MySqlException ex)
			{
				Console.WriteLine(ex);
			}
		}

		protected async Task InsertAsync(string query, params object[] parameters)
		{
			try
			{
				await OpenConnection(async connection =>
				{
					using (MySqlCommand command = connection.CreateCommand())
					{
						command.CommandText = query;
						SetupParameters(command.Parameters, parameters);

						await command.ExecuteNonQueryAsync();
					}
				});
			}
			catch (MySqlException ex)
			{
				Console.WriteLine(ex);
			}
		}

		private static void SetupParameters(MySqlParameterCollection collection, params object[] parameters)
		{
            for (int i = 0; i < parameters.Length; i++)
            {
                collection.AddWithValue($"@{i}", parameters[i]);
            }
		}
	}

	public static class DaoExtensions
	{
		public static T ReadData<T>(this DbDataReader reader, string columnName) =>
			(T)reader[columnName];

		public static bool ReadBool(this DbDataReader reader, string columnName) =>
			reader[columnName].Equals("1");
	}
}
