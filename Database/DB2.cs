//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Kinmatch.Entities;
//using MySql.Data.MySqlClient;

//namespace Kinmatch.Database
//{
//    internal class DB2
//    {
//        public async Task<string> test()
//        {
//            Console.WriteLine("startdb");
//            await Task.Delay(20000);
//            Console.WriteLine("enddb");
//            return "xyu";
//        }
//        public async Task<MySqlConnection> GetConnection()
//        {
//            string server = Config.DB.SERVER;
//            string database = Config.DB.DATABASE;
//            string uid = Config.DB.UID;
//            string password = Config.DB.PASSWORD;
//            string port = Config.DB.PORT;
//            string connectionString;
//            connectionString = "SERVER=" + server + "; DATABASE=" +
//            database + "; UID=" + uid + "; PASSWORD=" + password + "; PORT=" + port + ";";
//            MySqlConnection connection = new MySqlConnection(connectionString);
//            connection.ConnectionString = connectionString;
//            connection.Open();
//            return connection;
//        }
//        public async Task<Profile?> GetProfile(MySqlConnection connection, long tg_id)
//        {
//            var sql = "SELECT * FROM profiles WHERE tg_id = @tg_id LIMIT 1";
//            using var cmd = new MySqlCommand(sql, connection);
//            cmd.Parameters.AddWithValue("@tg_id", tg_id);
//            try
//            {
//                using var reader = await cmd.ExecuteReaderAsync();
//                if (await reader.ReadAsync())
//                {
//                    return new Profile(
//                        reader.IsDBNull("id") ? null : reader.GetInt32("id"),
//                        reader.IsDBNull("tg_id") ? null : reader.GetInt64("tg_id"),
//                        reader.IsDBNull("username") ? null : reader.GetString("username"),
//                        reader.IsDBNull("age") ? null : reader.GetInt32("age"),
//                        reader.IsDBNull("gender") ? null : reader.GetInt32("gender"),
//                        reader.IsDBNull("searchGender") ? null : reader.GetInt32("searchGender"),
//                        reader.IsDBNull("goal") ? null : reader.GetInt32("goal"),
//                        reader.IsDBNull("searchGoal") ? null : reader.GetInt32("searchGoal"),
//                        reader.IsDBNull("region") ? null : reader.GetString("region"),
//                        reader.IsDBNull("regionId") ? null : reader.GetInt32("regionId"),
//                        reader.IsDBNull("name") ? null : reader.GetString("name"),
//                        reader.IsDBNull("description") ? null : reader.GetString("description"),
//                        reader.IsDBNull("interests") ? null : reader.GetString("interests"),
//                        reader.IsDBNull("zodiac") ? null : reader.GetInt32("zodiac"),
//                        reader.IsDBNull("media") ? null : reader.GetString("media"),
//                        reader.IsDBNull("instagram") ? null : reader.GetString("instagram"),
//                        reader.IsDBNull("spotifyId") ? null : reader.GetInt32("spotifyId"),
//                        reader.IsDBNull("coins") ? null : reader.GetDouble("coins"),
//                        reader.IsDBNull("boosts") ? null : reader.GetInt32("boosts"),
//                        reader.IsDBNull("latitude") ? null : reader.GetDecimal("latitude"),
//                        reader.IsDBNull("longitude") ? null : reader.GetDecimal("longitude"),
//                        reader.IsDBNull("todayLikes") ? null : reader.GetInt32("todayLikes"),
//                        reader.IsDBNull("totalLikes") ? null : reader.GetInt32("totalLikes"),
//                        reader.IsDBNull("totalMatches") ? null : reader.GetInt32("totalMatches"),
//                        reader.IsDBNull("banned") ? null : reader.GetInt32("banned"),
//                        reader.IsDBNull("subscriptionType") ? null : reader.GetInt32("subscriptionType"),
//                        reader.IsDBNull("subscriptionUntil") ? null : reader.GetInt32("subscriptionUntil"),
//                        reader.IsDBNull("language") ? null : reader.GetString("language"),
//                        reader.IsDBNull("lastId") ? null : reader.GetInt32("lastId"),
//                        reader.IsDBNull("referer") ? null : reader.GetInt32("referer"),
//                        reader.IsDBNull("active") ? null : reader.GetInt32("active"),
//                        reader.IsDBNull("dateCreated") ? null : reader.GetInt32("dateCreated"),
//                        reader.IsDBNull("action") ? null : reader.GetString("action")
//                    );
//                }
//                else return null;
//            }
//            catch (Exception ex)
//            {
//                return new Profile(-2281488);
//            }
//        }

//        public async Task<bool> CreateProfile(MySqlConnection connection, Profile profile)
//        {
//            var sql = "INSERT INTO profiles (tg_id, username, age, gender, searchGender, goal, searchGoal, region, regionId, name, description, interests, zodiac, media, instagram, spotifyId, coins, boosts, latitude, longitude, todayLikes, totalLikes, totalMatches, banned, subscriptionType, subscriptionUntil, language, lastId, referer, active, dateCreated, action) " +
//                      "VALUES (@tg_id, @username, @age, @gender, @searchGender, @goal, @searchGoal, @region, @regionId, @name, @description, @interests, @zodiac, @media, @instagram, @spotifyId, @coins, @boosts, @latitude, @longitude, @todayLikes, @totalLikes, @totalMatches, @banned, @subscriptionType, @subscriptionUntil, @language, @lastId, @referer, @active, @dateCreated, @action)";
//            using var cmd = new MySqlCommand(sql, connection);
//            cmd.Parameters.AddWithValue("@tg_id", profile.tg_id);
//            cmd.Parameters.AddWithValue("@username", profile.username);
//            cmd.Parameters.AddWithValue("@age", profile.age);
//            cmd.Parameters.AddWithValue("@gender", profile.gender);
//            cmd.Parameters.AddWithValue("@searchGender", profile.searchGender);
//            cmd.Parameters.AddWithValue("@goal", profile.goal);
//            cmd.Parameters.AddWithValue("@searchGoal", profile.searchGoal);
//            cmd.Parameters.AddWithValue("@region", profile.region);
//            cmd.Parameters.AddWithValue("@regionId", profile.regionId);
//            cmd.Parameters.AddWithValue("@name", profile.name);
//            cmd.Parameters.AddWithValue("@description", profile.description);
//            cmd.Parameters.AddWithValue("@interests", profile.interests);
//            cmd.Parameters.AddWithValue("@zodiac", profile.zodiac);
//            cmd.Parameters.AddWithValue("@media", profile.media);
//            cmd.Parameters.AddWithValue("@instagram", profile.instagram);
//            cmd.Parameters.AddWithValue("@spotifyId", profile.spotifyId);
//            cmd.Parameters.AddWithValue("@coins", profile.coins);
//            cmd.Parameters.AddWithValue("@boosts", profile.boosts);
//            cmd.Parameters.AddWithValue("@latitude", profile.latitude);
//            cmd.Parameters.AddWithValue("@longitude", profile.longitude);
//            cmd.Parameters.AddWithValue("@todayLikes", profile.todayLikes);
//            cmd.Parameters.AddWithValue("@totalLikes", profile.totalLikes);
//            cmd.Parameters.AddWithValue("@totalMatches", profile.totalMatches);
//            cmd.Parameters.AddWithValue("@banned", profile.banned);
//            cmd.Parameters.AddWithValue("@subscriptionType", profile.subscriptionType);
//            cmd.Parameters.AddWithValue("@subscriptionUntil", profile.subscriptionUntil);
//            cmd.Parameters.AddWithValue("@language", profile.language);
//            cmd.Parameters.AddWithValue("@lastId", profile.lastId);
//            cmd.Parameters.AddWithValue("@referer", profile.referer);
//            cmd.Parameters.AddWithValue("@active", profile.active);
//            cmd.Parameters.AddWithValue("@dateCreated", profile.dateCreated);
//            cmd.Parameters.AddWithValue("@action", profile.action);

//            int rowsAffected = await cmd.ExecuteNonQueryAsync();
//            return rowsAffected > 0;
//        }
//        public async Task<bool> UpdateFullProfile(MySqlConnection connection, Profile profile)
//        {
//            var sql = "UPDATE profiles SET username = @username, age = @age, gender = @gender, searchGender = @searchGender, goal = @goal, searchGoal = @searchGoal, region = @region, regionId = @regionId, name = @name, description = @description, interests = @interests, zodiac = @zodiac, media = @media, instagram = @instagram, spotifyId = @spotifyId, coins = @coins, boosts = @boosts, latitude = @latitude, longitude = @longitude, todayLikes = @todayLikes, totalLikes = @totalLikes, totalMatches = @totalMatches, banned = @banned, subscriptionType = @subscriptionType, subscriptionUntil = @subscriptionUntil, language = @language, lastId = @lastId, referer = @referer, active = @active, action = @action WHERE tg_id = @tg_id";
//            using var cmd = new MySqlCommand(sql, connection);
//            cmd.Parameters.AddWithValue("@tg_id", profile.tg_id);
//            cmd.Parameters.AddWithValue("@username", profile.username);
//            cmd.Parameters.AddWithValue("@age", profile.age);
//            cmd.Parameters.AddWithValue("@gender", profile.gender);
//            cmd.Parameters.AddWithValue("@searchGender", profile.searchGender);
//            cmd.Parameters.AddWithValue("@goal", profile.goal);
//            cmd.Parameters.AddWithValue("@searchGoal", profile.searchGoal);
//            cmd.Parameters.AddWithValue("@region", profile.region);
//            cmd.Parameters.AddWithValue("@regionId", profile.regionId);
//            cmd.Parameters.AddWithValue("@name", profile.name);
//            cmd.Parameters.AddWithValue("@description", profile.description);
//            cmd.Parameters.AddWithValue("@interests", profile.interests);
//            cmd.Parameters.AddWithValue("@zodiac", profile.zodiac);
//            cmd.Parameters.AddWithValue("@media", profile.media);
//            cmd.Parameters.AddWithValue("@instagram", profile.instagram);
//            cmd.Parameters.AddWithValue("@spotifyId", profile.spotifyId);
//            cmd.Parameters.AddWithValue("@coins", profile.coins);
//            cmd.Parameters.AddWithValue("@boosts", profile.boosts);
//            cmd.Parameters.AddWithValue("@latitude", profile.latitude);
//            cmd.Parameters.AddWithValue("@longitude", profile.longitude);
//            cmd.Parameters.AddWithValue("@todayLikes", profile.todayLikes);
//            cmd.Parameters.AddWithValue("@totalLikes", profile.totalLikes);
//            cmd.Parameters.AddWithValue("@totalMatches", profile.totalMatches);
//            cmd.Parameters.AddWithValue("@banned", profile.banned);
//            cmd.Parameters.AddWithValue("@subscriptionType", profile.subscriptionType);
//            cmd.Parameters.AddWithValue("@subscriptionUntil", profile.subscriptionUntil);
//            cmd.Parameters.AddWithValue("@language", profile.language);
//            cmd.Parameters.AddWithValue("@lastId", profile.lastId);
//            cmd.Parameters.AddWithValue("@referer", profile.referer);
//            cmd.Parameters.AddWithValue("@active", profile.active);
//            cmd.Parameters.AddWithValue("@dateCreated", profile.dateCreated);
//            cmd.Parameters.AddWithValue("@action", profile.action);
//            int rowsAffected = await cmd.ExecuteNonQueryAsync();
//            return rowsAffected > 0;
//        }
//        public async Task<bool> UpdateProfileColumn(MySqlConnection connection, string column, object value, long? tg_id)
//        {
//            var sql = $"UPDATE profiles SET {column}=@value WHERE tg_id=@tg_id";
//            using var cmd = new MySqlCommand(sql, connection);
//            cmd.Parameters.AddWithValue("@value", Convert.ChangeType(value, GetColumnType(column)));
//            cmd.Parameters.AddWithValue("@tg_id", tg_id);
//            int rowsAffected = await cmd.ExecuteNonQueryAsync();
//            return rowsAffected > 0;
//        }

//        private Type GetColumnType(string column)
//        {
//            switch (column)
//            {
//                case "age":
//                    return typeof(int);
//                case "latitude":
//                case "longitude":
//                    return typeof(double);
//                // add more cases for other columns with non-string types
//                default:
//                    return typeof(string);
//            }
//        }




//    }
//}
