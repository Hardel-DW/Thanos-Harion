using Harion.Utility.Utils;
using HarmonyLib;
using Hazel;
using System.Linq;
using UnityEngine;
using TimeCore = ThanosHarion.Core.System.Time.Time;
using SnapButton = ThanosHarion.Core.Buttons.SnapButton;
using ThanosHarion.Core;

namespace ThanosHarion.Patch {
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.HandleRpc))]
    class HandleRpcPatch {

        public static bool Prefix([HarmonyArgument(0)] byte callId, [HarmonyArgument(1)] MessageReader reader) {
            if (callId == (byte) CustomRPC.TimeRewind) {
                TimeCore.isRewinding = true;
                PlayerControl.LocalPlayer.moveable = false;
                HudManager.Instance.FullScreen.color = new Color(0f, 0.5f, 0.8f, 0.3f);
                HudManager.Instance.FullScreen.enabled = true;
                return false;
            }

            if (callId == (byte) CustomRPC.TimeRevive) {
                PlayerControl player = PlayerControlUtils.FromPlayerId(reader.ReadByte());
                player.Revive();
                var body = Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == player.PlayerId);

                if (body != null)
                    Object.Destroy(body.gameObject);

                return false;
            }

            if (callId == (byte) CustomRPC.Snap) {
                PlayerControl player = PlayerControlUtils.FromPlayerId(reader.ReadByte());
                SnapButton.Instance.StartSnap();

                return false;
            }

            if (callId == (byte) CustomRPC.SyncroStone) {
                int ClientId = reader.ReadInt32();
                Vector3 Position = reader.ReadVector3();
                StoneData StoneData = (StoneData) reader.ReadByte();

                StoneInformation.ReadSyncroData(StoneData, Position, ClientId);
                return false;
            }

            if (callId == (byte) CustomRPC.SyncroDestroy) {
                StoneData StoneData = (StoneData) reader.ReadByte();
                
                StoneInformation.ReadDestroyData(StoneData);
                return false;
            }

            return true;
        }
    }
}