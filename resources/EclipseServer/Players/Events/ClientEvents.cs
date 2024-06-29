using AltV.Net;
using EclipseGTA.Handlers;
using System.Numerics;

namespace EclipseGTA.Players.Events
{
    public class ClientEvents : IScript
    {

        [ClientEvent("CLIENT::SERVER::WaypointCoord")]
        public static void WaypointCoord(ExtPlayer player, Vector3 coord)
        {
            player.Position = coord;
        }

        [ClientEvent("CLIENT::SERVER::TryLogin")]
        public static void TryAuthEvent(ExtPlayer player, string login, string password)
        {
            Server.RunTask(async () =>
            {
                await Auth.TryLoginAccount(player, login, password);
            });
        }

        [ClientEvent("CLIENT::SERVER::TryReg")]
        public static void TryRegEvent(ExtPlayer player, string login, string email, string password, string promo)
        {
            Server.RunTask(async () =>
            {
                await Auth.TryRegAccount(player, login, email, password, promo);
            });
        }
    }
}
