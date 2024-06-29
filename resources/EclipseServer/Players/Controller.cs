using AltV.Net.Enums;
using EclipseGTA.Handlers;
using EclipseGTA.Players.data;

namespace EclipseGTA.Players
{
    public static class Controller
    {
        public static bool IsHaveSessionData(this ExtPlayer player)
        {
            if (player is null) return false;

            return player.SessionData != null;
        }

        public static void SetAuth(this ExtPlayer player)
        {
            if (player is null) return;

            player.Dimension = (int)player.Id + 333;
            player.Emit("SERVER::CLIENT::AuthClient");
        }

        public static void SetData(this ExtPlayer player)
        {
            if (player is null) return;

            if (!Server.PlayerIdToPlayerEntity.ContainsKey(player.Id))
                Server.PlayerIdToPlayerEntity.TryAdd(player.Id, player);

            SessionData sessionData = new()
            {
                Id = player.Id,
                Dimension = 0,
                HWID = $"{player.HardwareIdHash}_{player.HardwareIdExHash}",
                SocialClubName = player.SocialClubName,
                SocialClubId = player.SocialClubId,
                IP = player.Ip,
                IsConnect = true,
                isLogin = true
            };

            player.SetSessionData(sessionData);
            player.Model = (uint)PedModel.MilitaryBum;
            player.Dimension = 0;
        }
    }
}
