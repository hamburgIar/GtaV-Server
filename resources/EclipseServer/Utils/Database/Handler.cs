using AltV.Net;
using MySql.Data.MySqlClient;
using System.Data;

namespace EclipseGTA.Utils.Database
{
    public class Handler
    {
        private static readonly string connString = "server=localhost;user=root;database=eclipse;password=;Pooling=true;";

        private static readonly MySqlConnection connection = new(connString);

        public static async Task Start()
        {
            try
            {
                await connection.OpenAsync();
                Alt.LogInfo("[DB] Подключение к MySQL успешно!");
            }
            catch (Exception ex)
            {
                Alt.LogError($"[DB] Error: {ex}");
            }
        }

        public static async Task Query(MySqlCommand command)
        {
            if (command == null || string.IsNullOrEmpty(command.CommandText)) return;

            try
            {
                command.Connection = connection;
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Alt.LogError($"[DB] Error Query: {ex}");
            }
        }

        public static async Task<DataTable?> QueryRead(MySqlCommand command)
        {
            if (command == null || string.IsNullOrEmpty(command.CommandText)) return null;

            try
            {
                command.Connection = connection;
                using var reader = await command.ExecuteReaderAsync();
                var dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (Exception ex)
            {
                Alt.LogError($"[DB] Error QueryRead: {ex}");
                return null;
            }
        }

        public static async Task<object?> QueryReadScalar(MySqlCommand command)
        {
            if (command == null || string.IsNullOrEmpty(command.CommandText)) return null;

            try
            {
                command.Connection = connection;
                object result = await command.ExecuteScalarAsync();
                return result; 
            }
            catch (Exception ex)
            {
                Alt.LogError($"[DB] Error QueryReadScalar: {ex}");
                return null;
            }
        }

        public static async Task<bool> IsUserRegistered(MySqlCommand command)
        {
            if (command == null || string.IsNullOrEmpty(command.CommandText)) return false;

            try
            {
                command.Connection = connection;
                object result = await command.ExecuteScalarAsync();
                return result != null && !string.IsNullOrEmpty(result.ToString());
            }
            catch (Exception ex)
            {
                Alt.LogError($"[DB] Error IsUserRegistered: {ex}");
                return false;
            }
        }
    }
}
