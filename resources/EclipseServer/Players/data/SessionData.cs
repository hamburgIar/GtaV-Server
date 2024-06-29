namespace EclipseGTA.Players.data
{
    public class SessionData
    {
        public uint Id { get; set; } = 0;
        public int Dimension { get; set; } = 0;
        public string HWID { get; set; } = "none";
        public string SocialClubName { get; set; } = "none";
        public ulong SocialClubId { get; set; } = 0;
        public string IP { get; set; } = "none";
        public bool IsConnect { get; set; } = false;
        public bool isLogin { get; set; } = false;
    }
}
