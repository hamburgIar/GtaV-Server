using AltV.Net;
using AltV.Net.Elements.Entities;
using EclipseGTA.Players.data;

namespace EclipseGTA.Handlers
{
    public class ExtPlayer : Player
    {
        public ExtPlayer(ICore core, IntPtr playerPointer, uint id) : base(core, playerPointer, id) { }
        public SessionData SessionData { get; set; }
        public CharacterData CharacterData { get; set; }

        public void SetSessionData(SessionData sessionData)
        {
            SessionData = sessionData;
        }

        public void SetCharacterData(CharacterData characterData)
        {
            CharacterData = characterData;
        }
    }
}
