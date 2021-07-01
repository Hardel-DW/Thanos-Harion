using Harion.CustomOptions;

namespace ThanosHarion {
    public static class GenericGameOption {

        public static CustomOptionHolder RoleHolder = CustomOption.AddHolder("<b><color=#007ACCFF>Template Example Role :</color></b>");

        public static void GameOptionConfiguration() {
            RoleHolder.HudStringFormat = (option, name, value) => $"\n{name}";
        }
    }
}
