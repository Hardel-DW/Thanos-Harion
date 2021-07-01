using HarmonyLib;
using Hazel;

namespace ThanosHarion.Patch {
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.HandleRpc))]
    class HandleRpcPatch {

        public static bool Prefix([HarmonyArgument(0)] byte callId, [HarmonyArgument(1)] MessageReader reader) {
            if (callId == (byte) CustomRPC.TestRpc) {
                ThanosHarionPlugin.Logger.LogInfo("Hello World");

                return false;
            }

            return false;
        }
    }
}
