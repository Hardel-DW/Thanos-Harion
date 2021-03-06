using BepInEx;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using Harion;
using Harion.Cooldown;
using Harion.Reactor;
using HarmonyLib;

namespace ThanosHarion {

    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(HarionPlugin.Id)]
    public class ThanosHarionPlugin : BasePlugin {
        public const string Id = "fr.hardel.thanos";
        public static ManualLogSource Logger;

        public Harmony Harmony { get; } = new Harmony(Id);

        public override void Load() {
            Logger = Log;
            Harmony.PatchAll();
            RegisterInIl2CppAttribute.Register();
            RegisterCooldownButton.Register();
            ResourceLoader.LoadAssets();
            GenericGameOption.GameOptionConfiguration();
        }
    }
}
