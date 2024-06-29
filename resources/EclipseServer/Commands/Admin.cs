using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Resources.Chat.Api;
using EclipseGTA.Handlers;
using EclipseGTA.Players.data;
using Org.BouncyCastle.Asn1.X509;
using System.Numerics;

namespace EclipseGTA.Commands
{
    public class Admin : IScript
    {
        [Command("sethp")]
        public static void SetPlayerEntityHealth(ExtPlayer player, ushort id, ushort hp = 100)
        { 
            if (hp > 100)
            {
                player.SendChatMessage("Значение HP должно быть от 1 до 100!");
                return;
            }

            ExtPlayer? target = Server.GetPlayerByID(id);

            if (target == null)
            {
                player.SendChatMessage($"Игрок с ID {id} не найден!");
                return;
            } else if (hp == 0) {
                target.Health = 0;
            } else {
                target.Health = (ushort)(100 + hp); // HP От 100 до 200 на AltV
            }
        }

        [Command("setmehp")]
        public static void SetEntityHealth(ExtPlayer player, ushort hp = 100)
        {
            if (hp > 100)
            {
                player.SendChatMessage("Значение HP должно быть от 1 до 100!");
                return;
            }

            else if (hp == 0)
            {
                player.Health = 0;
            }
            else
            {
                player.Health = (ushort)(100 + hp); // HP От 100 до 200 на AltV
            }
        }

        [Command("getid")]
        public static void GetEntityId(ExtPlayer player) 
        {
            player.SendChatMessage($"Ваш ID: {player.Id}");
        }

        [Command("veh")]
        public static void SpawnVeh(ExtPlayer player, string model, byte seat = 0, byte r = 0, byte g = 0, byte b = 0) 
        {
            Position playerPos = player.Position;
            float yPos = playerPos.Y + (seat == 0 ? 3f : 0f);

            ExtVehicle vehicle = (ExtVehicle)Alt.CreateVehicle(Alt.Hash(model), new Position(playerPos.X, yPos, playerPos.Z), player.Rotation);
            if (vehicle == null)
            {
                player.SendChatMessage("Транспорт с таким названием не найден!");
            } else
            {
                vehicle.PrimaryColorRgb = new Rgba(r, g, b, 255);
                vehicle.SecondaryColorRgb = new Rgba(r, g, b, 255);

                if (seat > 0) player.SetIntoVehicle(vehicle, seat);

                player.SendChatMessage($"Вы заспавнили авто {model}");
            }
        }

        [Command("tpveh")]
        public static void TeleportVehicle(ExtPlayer player, uint id)
        {
            ExtVehicle vehicle = Server.GetVehicleByID(id);
            
            if (vehicle == null) 
            {
                player.SendChatMessage($"Транспорт с ID {id} не найден!");
                return;
            }
                
            vehicle.Position = player.Position; 
        }

        [Command("repveh")]
        public static void RepairVehicle(ExtPlayer player, uint id)
        {
            ExtVehicle vehicle = Server.GetVehicleByID(id);

            if (vehicle == null)
            {
                player.SendChatMessage($"Транспорт с ID {id} не найден!");
                return;
            }

            vehicle.Repair();
        }

        [Command("stveh")]
        public static void SeatIntoVeh(ExtPlayer player, uint id, byte seat = 1)
        {   
            if (seat >= 1)
            {
                ExtVehicle vehicle = Server.GetVehicleByID(id);

                if (vehicle == null)
                {
                    player.SendChatMessage($"Транспорт с ID {id} не найден!");
                    return;
                }

                player.SetIntoVehicle(vehicle, seat);
            }
        }

        [Command("frz")]
        public static void FreezeEntity(ExtPlayer player, uint id)
        {
            ExtPlayer? target = Server.GetPlayerByID(id);

            if (target == null)
            {
                player.SendChatMessage($"Игрок с ID {id} не найден!");
                return;
            }

            target.Emit("SERVER::CLIENT::FreezePlayer");
        }

        [Command("unfrz")]
        public static void UnFreezeEntity(ExtPlayer player, uint id)
        {
            ExtPlayer? target = Server.GetPlayerByID(id);

            if (target == null)
            {
                player.SendChatMessage($"Игрок с ID {id} не найден!");
                return;
            }

            target.Emit("SERVER::CLIENT::UnFreezePlayer");
        }

        [Command("pos")]
        public static void GetPos(ExtPlayer player)
        {
            Position pos = player.IsInVehicle ? player.Vehicle.Position : player.Position;
            Vector3 rot = player.IsInVehicle ? player.Vehicle.Rotation : player.Rotation;

            player.SendChatMessage($"PP: X: {pos.X} Y: {pos.Y} Z: {pos.Z}   PR: X: {rot.X} Y: {rot.Y} Z: {rot.Z}");
            Alt.LogInfo($"PP: X: {pos.X} Y: {pos.Y} Z: {pos.Z}   PR: X: {rot.X} Y: {rot.Y} Z: {rot.Z}");
            player.Emit("SERVER::CLIENT::GetCamPos");
        }

        [Command("getdata")]
        public static void GetUserData(ExtPlayer player, uint id)
        {
            ExtPlayer targetPlayer = Server.GetPlayerByID(id);
            if (targetPlayer == null)
            {
                player.SendChatMessage($"Пользователь с ID {id} не найден!");
                return;
            }

            SessionData sessionData = targetPlayer.SessionData;
            player.SendChatMessage($"ID: {sessionData.Id}, SCID: {sessionData.SocialClubId}, SCName: {sessionData.SocialClubName}, Dimension: {sessionData.Dimension}, IP: {sessionData.IP}, IsConn: {sessionData.IsConnect}");
        }

        [Command("setskin")]
        public static void SetEntityModel(ExtPlayer player, uint id, string modelName)
        {
            ExtPlayer targetPlayer = Server.GetPlayerByID(id);
            if (targetPlayer == null)
            {
                player.SendChatMessage($"Игрок с ID: {id} не найден!");
                return;
            }

            targetPlayer.Model = Alt.Hash(modelName);
        }

        [Command("setdim")]
        public static void SetEntityDimension(ExtPlayer player, uint id, int dimension)
        {
            ExtPlayer targetPlayer = Server.GetPlayerByID(id);
            if (targetPlayer == null)
            {
                player.SendChatMessage($"Игрок с ID: {id} не найден!");
                return;
            }

            targetPlayer.Dimension = dimension;
            targetPlayer.SessionData.Dimension = dimension;
        }

        [Command("wptp")]
        public static void WaypointTp(ExtPlayer player)
        {
            player.Emit("SERVER::CLIENT::GetWaypointCoord");
        }

        [Command("vehinfo")]
        public static void Vehicle3DInfo(ExtPlayer player)
        {
            player.Emit("SERVER::CLIENT::3DVehInfo");
        }

        [Command("crashveh")]
        public static void CrashVehicle(ExtPlayer player, uint id)
        {
            ExtVehicle vehicle = Server.GetVehicleByID(id);
            if (vehicle == null)
            {
                player.SendChatMessage($"Транспорт с ID {id} не найден");
                return;
            }

            vehicle.SetTimedExplosion(true, player, 1);
        }

        [Command("delveh")]
        public static void DeleteVehicle(ExtPlayer player, uint id)
        {
            ExtVehicle vehicle = Server.GetVehicleByID(id);
            if (vehicle == null)
            {
                player.SendChatMessage($"Транспорт с ID {id} не найден");
                return;
            }

            vehicle.Destroy();
            Server.VehicleIdToVehicleEntity.Remove(id, out _);
        }

        [Command("kill")]
        public static void KillPlayer(ExtPlayer player, uint id)
        {
            ExtPlayer targetPlayer = Server.GetPlayerByID(id);
            if (targetPlayer == null)
            {
                player.SendChatMessage($"Игрок с ID {id} не найден");
                return;
            }

            targetPlayer.Health = 0;
        }

        [Command("anim")]
        public static void PlayAnim(ExtPlayer player, uint id, string animDict, string animName, int flags)
        {
            ExtPlayer targetPlayer = Server.GetPlayerByID(id);
            if (targetPlayer == null)
            {
                player.SendChatMessage($"Игрок с ID {id} не найден");
                return;
            }

            targetPlayer.PlayAnimation(animDict, animName, 8.0f, 8.0f, -1, flags, 0f, false, false, false);
        }

        [Command("res")]
        public static void ResPlayer(ExtPlayer player, uint id)
        {
            ExtPlayer targetPlayer = Server.GetPlayerByID(id);
            if (targetPlayer == null)
            {
                player.SendChatMessage($"Игрок с ID {id} не найден");
                return;
            }

            if (!targetPlayer.IsDead) return;

            Position newPos = new(targetPlayer.Position.X, targetPlayer.Position.Y, targetPlayer.Position.Z - 0.1f);
        
            targetPlayer.Spawn(newPos, 0);
            targetPlayer.PlayAnimation("get_up@standard", "back", 8.0f, 8.0f, -1, 4|8, 0f, false, false, false);
        }

        [Command("setarmor")]
        public static void SetArmor(ExtPlayer player, uint id, ushort armor = 100)
        {
            ExtPlayer targetPlayer = Server.GetPlayerByID(id);
            if (targetPlayer == null)
            {
                player.SendChatMessage($"Пользователь с ID {id} не найден");
                return;
            }

            targetPlayer.Armor = armor;
        }
    }
}
