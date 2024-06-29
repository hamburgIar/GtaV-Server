using AltV.Net;
using AltV.Net.Elements.Entities;

namespace EclipseGTA.Handlers
{
    class ExtVehicleFactory : IEntityFactory<IVehicle>
    {
        public IVehicle Create(ICore core, IntPtr nativePointer, uint id) 
        {
            ExtVehicle vehicle = new(core, nativePointer, id);
            Server.VehicleIdToVehicleEntity.TryAdd(vehicle.Id, vehicle);
            return vehicle;
        }
    }
}
