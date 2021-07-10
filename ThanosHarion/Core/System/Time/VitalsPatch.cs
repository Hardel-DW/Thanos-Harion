using HarmonyLib;
using UnityEngine;
using Button = ThanosHarion.Core.Buttons.TimeButton;
using ThanosRole = ThanosHarion.Core.Roles.Thanos;

namespace ThanosHarion.Core.System.Time {

    [HarmonyPatch(typeof(VitalsMinigame), nameof(VitalsMinigame.Begin))]
    class VitalsPatch {

        public static bool Prefix(VitalsMinigame __instance) {
            if ((ThanosRole.UsableVitals.GetValue() || Button.Instance.UseNumber == 0) && ThanosRole.Instance.HasRole(PlayerControl.LocalPlayer)) {
                Object.Destroy(__instance.gameObject);
                return false;
            }

            return true;
        }
    }
}