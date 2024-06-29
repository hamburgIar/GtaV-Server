using AltV.Net;
using AltV.Net.Data;
using EclipseGTA.Handlers;
using EclipseGTA.Players;
using EclipseGTA.Utils.Database;
using MySql.Data.MySqlClient;

namespace EclipseGTA.Events
{
    public class Connected : IScript
    {
        [ScriptEvent(ScriptEventType.PlayerConnect)]
        public static async void OnPlayerConnected(ExtPlayer player, string reason)
        {
            if (player == null) return;

            if (player.IsHaveSessionData())
            {
                player.Kick("Перезайдите на сервер.");
                return;
            }

            if (Server.PlayerIdToPlayerEntity.ContainsKey(player.Id))
                Server.PlayerIdToPlayerEntity.TryRemove(player.Id, out _);

            DateTime startTime = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 21, 0, 0);
            
            player.SetDateTime(startTime);
            player.Spawn(new Position(383.63077f, -368.42636f, 46.837402f), 0);

            string query = "SELECT last_login FROM accounts WHERE socialclubid = @socialclubid";
            using MySqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("@socialclubid", player.SocialClubId);

            object result = await Handler.QueryReadScalar(cmd);

            if (result != null)
            {
                DateTime lastLogin = Convert.ToDateTime(result);
                DateTime now = DateTime.Now;

                if (now.Subtract(lastLogin).Days < 10)
                {
                    player.SetData();

                    return;
                }
            } 

            player.SetAuth();
        }
    }
}
