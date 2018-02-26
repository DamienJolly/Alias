using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Users
{
    public class UserDatabase
    {
		public static object Variable(int userId, string column)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", userId);
				return dbClient.DataRow("SELECT `" + column + "` FROM `habbos` WHERE `id` = @id")[column];
			}
		}

		public static int Id(string username)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("username", username);
				return dbClient.Int32("SELECT `id` FROM `habbos` WHERE `Username` = @username");
			}
		}
	}
}
