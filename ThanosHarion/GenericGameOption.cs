using Harion.CustomOptions;

namespace ThanosHarion {
    public static class GenericGameOption {

        public static CustomOptionHolder ThanosHolder = CustomOption.AddHolder("<b><color=#5C3090FF>Thanos Option :</color></b>");
        public static CustomOptionHolder TimeStoneHolder = CustomOption.AddHolder("<b><color=#5C3090FF>Time Stone Option :</color></b>", true, true, false, ThanosHolder);
        public static CustomOptionHolder RealityStoneHolder = CustomOption.AddHolder("<b><color=#5C3090FF>Reality Stone Option :</color></b>", true, true, false, ThanosHolder);
        public static CustomOptionHolder SpaceStoneHolder = CustomOption.AddHolder("<b><color=#5C3090FF>Space Stone Option :</color></b>", true, true, false, ThanosHolder);
        public static CustomOptionHolder MindStoneHolder = CustomOption.AddHolder("<b><color=#5C3090FF>Mind Stone Option :</color></b>", true, true, false, ThanosHolder);
        public static CustomOptionHolder SoulStoneHolder = CustomOption.AddHolder("<b><color=#5C3090FF>Soul Stone Option :</color></b>", true, true, false, ThanosHolder);
        public static CustomOptionHolder PowerStoneHolder = CustomOption.AddHolder("<b><color=#5C3090FF>Power Stone Option :</color></b>", true, true, false, ThanosHolder);
        public static CustomOptionHolder SnapHolder = CustomOption.AddHolder("<b><color=#5C3090FF>Snap Option :</color></b>", true, true, false, ThanosHolder);

        public static void GameOptionConfiguration() {
            ThanosHolder.HudStringFormat = (option, name, value) => $"\n{name}";
        }
    }
}
