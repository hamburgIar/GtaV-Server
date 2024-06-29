using EclipseGTA.Handlers;

namespace EclipseGTA.Utils
{
    public class Notify
    {
        public enum NotifyType
        {
            Alert
        }

        public enum NotifyPosition
        {
            TopRight
        }

        public static void SendNotify(ExtPlayer player, NotifyType type, string mess, int time)
        {
            player.Emit("SERVER::CLIENT::Notify", Convert.ToString(type), mess, time);
        }
    }
}
