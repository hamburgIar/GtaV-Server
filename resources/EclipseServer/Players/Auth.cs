using EclipseGTA.Handlers;
using EclipseGTA.Utils;
using EclipseGTA.Utils.Database;
using MySql.Data.MySqlClient;
using System.Data;

namespace EclipseGTA.Players
{
    public class Auth
    {
        public static async Task TryLoginAccount(ExtPlayer player, string login, string password)
        {
            string query = "SELECT login, password, promo, last_login FROM accounts WHERE login = @login";
            using MySqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("@login", login);

            DataTable dataTable = await Handler.QueryRead(cmd);

            if (dataTable.Rows.Count == 0) { player.Emit("SERVER::CLIENT::AuthError", "Пользователь не найден!"); return; }

            DataRow dr = dataTable.Rows[0];

            string accLogin = dr[0].ToString();
            string accPass = dr[1].ToString();

            if (password != accPass) { player.Emit("SERVER::CLIENT::AuthError", "Неверный пароль!"); return; }

            string promo = dr[2].ToString();

            DateTime lastLogin = Convert.ToDateTime(dr[3]);

            if (lastLogin != DateTime.Now)
            {
                query = "UPDATE accounts SET last_login = @new_login_date WHERE socialclubid = @socialclubid";
                using MySqlCommand updateCommand = new(query);
                updateCommand.Parameters.AddWithValue("@new_login_date", DateTime.Now);
                updateCommand.Parameters.AddWithValue("@socialclubid", player.SocialClubId);

                await Handler.Query(updateCommand);
            }

            player.SetData();
            player.Emit("SERVER::CLIENT::SLogin");

            Notify.SendNotify(player, Notify.NotifyType.Alert, "SLogin!", 3000);
        }

        public static async Task TryRegAccount(ExtPlayer player, string login, string email, string pass, string promo)
        {
            string query = "SELECT 0 FROM accounts WHERE socialclubid = @socialclubid";
            using MySqlCommand command = new(query);
            command.Parameters.AddWithValue("@socialclubid", player.SocialClubId);

            bool isReg = await Handler.IsUserRegistered(command);

            if (!isReg)
            {
                query = "INSERT INTO accounts (socialclubid, login, password, email, promo, ip, last_login) VALUES (@socialclubid, @login, @password, @email, @promo, @ip, @last_login)";
                using MySqlCommand insertCommand = new(query);
                insertCommand.Parameters.AddWithValue("@socialclubid", player.SocialClubId);
                insertCommand.Parameters.AddWithValue("@login", login);
                insertCommand.Parameters.AddWithValue("@password", pass);
                insertCommand.Parameters.AddWithValue("@email", email);
                insertCommand.Parameters.AddWithValue("@promo", promo);
                insertCommand.Parameters.AddWithValue("@ip", player.Ip);
                insertCommand.Parameters.AddWithValue("@last_login", DateTime.Now);

                await Handler.Query(insertCommand);

                if (!Server.PlayerIdToPlayerEntity.ContainsKey(player.Id))
                    Server.PlayerIdToPlayerEntity.TryAdd(player.Id, player);

                player.SetData();
                player.Emit("SERVER::CLIENT::SRegistration");
            }
            else
            {
                player.Emit("SERVER::CLIENT::AuthError", "Пользователь с вашим socialclubid уже зарегистрирован!");
            }
        }
    }
}
