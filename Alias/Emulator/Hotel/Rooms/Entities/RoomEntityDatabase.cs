using System.Collections.Generic;
using Alias.Emulator.Database;
using Alias.Emulator.Utilities;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Rooms.Entities
{
    class RoomEntityDatabase
    {
		public static List<RoomEntity> ReadBots(Room room)
		{
			List<RoomEntity> bots = new List<RoomEntity>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("roomId", room.Id);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `bots` INNER JOIN `bots_room_data` ON `bots`.`id` = `bots_room_data`.`id` WHERE `bots`.`room_id` = @roomId"))
				{
					while (Reader.Read())
					{
						RoomEntity entity = new RoomEntity
						{
							Id = Reader.GetInt32("id"),
							Name = Reader.GetString("name"),
							Motto = Reader.GetString("motto"),
							Look = Reader.GetString("look"),
							Gender = Reader.GetString("gender"),
							OwnerId = Reader.GetInt32("user_id"),
							DanceId = Reader.GetInt32("dance_id"),
							EffectId = Reader.GetInt32("effect_id"),
							CanWalk = Reader.GetBoolean("can_walk"),
							Type = RoomEntityType.Bot,
							Room = room,
							Position = new UserPosition()
							{
								X = Reader.GetInt32("x"),
								Y = Reader.GetInt32("y"),
								Z = Reader.GetDouble("z"),
								Rotation = Reader.GetInt32("rot"),
								HeadRotation = Reader.GetInt32("rot")
							}
						};
						bots.Add(entity);
					}
				}
			}
			return bots;
		}

		public static void UpdateBot(RoomEntity bot)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("botId", bot.Id);
				dbClient.AddParameter("look", bot.Look);
				dbClient.AddParameter("gender", bot.Gender);
				dbClient.AddParameter("name", bot.Name);
				dbClient.AddParameter("danceId", bot.DanceId);
				dbClient.AddParameter("effectId", bot.EffectId);
				dbClient.AddParameter("canWalk", DatabaseBoolean.GetBoolFromString(bot.CanWalk));
				dbClient.Query("UPDATE `bots` SET `name` = @name, `look` = @look, `gender` = @gender, `dance_id` = @danceId, `effect_id` = @effectId, `can_walk` = @canWalk WHERE `id` = @botId");
			}
		}

		public static void AddBot(RoomEntity bot)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("botId", bot.Id);
				dbClient.AddParameter("xPos", bot.Position.X);
				dbClient.AddParameter("yPos", bot.Position.Y);
				dbClient.AddParameter("ZPos", bot.Position.Z);
				dbClient.AddParameter("Rot", bot.Position.Rotation);
				dbClient.Query("INSERT INTO `bots_room_data` (`id`, `x`, `y`, `z`, `rot`) VALUES (@botId, @xPos, @yPos, @zPos, @rot)");
			}
		}

		public static void RemoveBot(RoomEntity bot)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("botId", bot.Id);
				dbClient.Query("DELETE FROM `bots_room_data` WHERE `id` = @botId");
			}
		}

		public static List<RoomEntity> ReadPets(Room room)
		{
			List<RoomEntity> pets = new List<RoomEntity>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("roomId", room.Id);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `pets` INNER JOIN `pets_room_data` ON `pets`.`id` = `pets_room_data`.`id` WHERE `pets`.`room_id` = @roomId"))
				{
					while (Reader.Read())
					{
						RoomEntity entity = new RoomEntity
						{
							Id = Reader.GetInt32("id"),
							Name = Reader.GetString("name"),
							Motto = "",
							Look = Reader.GetInt32("type") + " " + Reader.GetInt32("race") + " " + Reader.GetString("colour") + " 2 2 4 0 0",
							Gender = Reader.GetInt32("type") + "",
							OwnerId = Reader.GetInt32("user_id"),
							Type = RoomEntityType.Pet,
							Room = room,
							Position = new UserPosition()
							{
								X = Reader.GetInt32("x"),
								Y = Reader.GetInt32("y"),
								Z = Reader.GetDouble("z"),
								Rotation = Reader.GetInt32("rot"),
								HeadRotation = Reader.GetInt32("rot")
							}
						};
						pets.Add(entity);
					}
				}
			}
			return pets;
		}

		public static void AddPet(RoomEntity pet)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("petId", pet.Id);
				dbClient.AddParameter("xPos", pet.Position.X);
				dbClient.AddParameter("yPos", pet.Position.Y);
				dbClient.AddParameter("ZPos", pet.Position.Z);
				dbClient.AddParameter("Rot", pet.Position.Rotation);
				dbClient.Query("INSERT INTO `pets_room_data` (`id`, `x`, `y`, `z`, `rot`) VALUES (@petId, @xPos, @yPos, @zPos, @rot)");
			}
		}

		public static void RemovePet(RoomEntity pet)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("petId", pet.Id);
				dbClient.Query("DELETE FROM `pets_room_data` WHERE `id` = @petId");
			}
		}
	}
}
