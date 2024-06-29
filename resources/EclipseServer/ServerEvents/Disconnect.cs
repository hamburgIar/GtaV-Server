using AltV.Net;
using EclipseGTA.Handlers;

namespace EclipseGTA.Events
{
    class Disconnect : IScript
    {
        [ScriptEvent(ScriptEventType.PlayerDisconnect)]
        public static void OnPlayerDisconnect(ExtPlayer player, string reason)
        {
            if (Server.PlayerIdToPlayerEntity.ContainsKey(player.Id))
                Server.PlayerIdToPlayerEntity.TryRemove(player.Id, out _);
        }
    }
}
