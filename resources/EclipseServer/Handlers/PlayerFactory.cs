using AltV.Net;
using AltV.Net.Elements.Entities;

namespace EclipseGTA.Handlers
{
    class CharacterFactory : IEntityFactory<IPlayer>
    {
        public IPlayer Create(ICore core, IntPtr nativePointer, uint id) => new ExtPlayer(core, nativePointer, id);
    }
}
