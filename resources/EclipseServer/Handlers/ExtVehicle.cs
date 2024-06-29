using AltV.Net;
using AltV.Net.Elements.Entities;

namespace EclipseGTA.Handlers
{
    class ExtVehicle : Vehicle
    {
        public ExtVehicle(ICore core, IntPtr nativePointer, uint id) : base(core, nativePointer, id) { }
    }
}
