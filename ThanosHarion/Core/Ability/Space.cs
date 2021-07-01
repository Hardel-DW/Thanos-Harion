using Harion.Utility.Utils;
using Harion.Cooldown;
using ThanosRoles = ThanosHarion.Thanos;

namespace ThanosHarion {

    [RegisterCooldownButton]
    public class Button : CustomButton<Button> {

        public override void OnCreateButton() {
            Timer = 10f;
            MaxTimer = 10f;
            UseNumber = 4;
            Roles = ThanosRoles.Instance;
            SetSprite("ThanosHarion.Resources.space.png", 300);
            DecreamteUseNimber = UseNumberDecremantion.OnClick;
            Key = ThanosRoles.KeyBindSpace.Key;
        }

        public override void OnClick() => VentUtils.PlaceVent(PlayerControl.LocalPlayer.transform.position);
    }
}