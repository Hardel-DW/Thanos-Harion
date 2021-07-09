using Harion.Utility.Utils;
using HarmonyLib;
using Hazel;
using System.Linq;
using UnityEngine;
using TimeCore = ThanosHarion.Core.System.Time.Time;
using SnapButton = ThanosHarion.Core.Buttons.SnapButton;

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

            return true;
        }
    }
}