using AltV.Net;
using AltV.Net.Elements.Entities;
using System.Collections.Concurrent;
using EclipseGTA.Handlers;
using EclipseGTA.Utils.Database;

namespace EclipseGTA
{
    class Server : Resource
    {
        public static ConcurrentDictionary<uint, ExtPlayer> PlayerIdToPlayerEntity = new();
        public static ConcurrentDictionary<uint, ExtVehicle> VehicleIdToVehicleEntity = new();

        public override void OnStart()
        {
            RunTask(async () =>
            {
                await Handler.Start();
            });
            Alt.LogInfo("Server has been started");
        }

        public override void OnStop()
        {
            Alt.LogInfo("Server has been terminated");
        }

        public static ExtPlayer? GetPlayerByID(uint id)
        {
            if (!PlayerIdToPlayerEntity.ContainsKey(id)) return null;

            return PlayerIdToPlayerEntity[id];
        }

        public static ExtVehicle? GetVehicleByID(uint id)
        {
            if(!VehicleIdToVehicleEntity.ContainsKey(id)) return null;

            return VehicleIdToVehicleEntity[id];
        }

        public override IEntityFactory<IPlayer> GetPlayerFactory() => new CharacterFactory();
        public override IEntityFactory<IVehicle> GetVehicleFactory() => new ExtVehicleFactory();

        public static void RunTask(Func<Task> action)
        {
            try
            {
                var task = action.Invoke();
                task.Wait(); 
            }
            catch (Exception ex)
            {
                Alt.LogError("[SERVER] RunTask error: " + ex.Message);
            }
        }
    }
}
