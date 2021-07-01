using Harion.Utility.Utils;
using Harion.Cooldown;
using SuperJesterRoles = ThanosHarion.Thanos;

namespace ThanosHarion {

    [RegisterCooldownButton]
    public class Button : CustomButton<Button> {

        public override void OnCreateButton() {
            Timer = 10f;
            MaxTimer = 10f;
            UseNumber = 4;
            Roles = SuperJesterRoles.Instance;
            SetSprite("ThanosHarion.Resources.Example.png", 125);
            DecreamteUseNimber = UseNumberDecremantion.OnClick;
            Key = SuperJesterRoles.KeyBindExample.Key;
        }

        public override void OnClick() => VentUtils.PlaceVent(PlayerControl.LocalPlayer.transform.position);
    }
}