using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using EclipseGTA.Handlers;

namespace EclipseGTA.Players.Events
{
    public class ScriptEvents : IScript
    {
        [ScriptEvent(ScriptEventType.PlayerDead)]
        public static void OnPlayerdDead(ExtPlayer player, ExtPlayer killer, uint weapon)
        {
            if (player.IsInVehicle)
            {
                player.Emit("SERVER::CLIENT::LeaveVehicle");
            }

            if (killer == null) { return; }
        }
    }
}
