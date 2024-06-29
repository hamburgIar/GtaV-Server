namespace EclipseGTA.Players.data
{
    public class CharacterData
    {
        public int staticID { get; set; } = 0;
        public string username { get; set; } = "none";
        public string password { get; set; } = "none";
        public string name { get; set; } = "none";
        public string surname { get; set; } = "none";
        public int balance { get; set; } = 0;
        public bool isAdmin { get; set; } = false;
        public int adminLvl { get; set; } = 0;
        public Dictionary<int, object> inventory { get; set; }
    }
}
