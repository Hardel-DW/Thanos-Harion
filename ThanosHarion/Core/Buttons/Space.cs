using Harion.Utility.Utils;
using Harion.Cooldown;
using ThanosRoles = ThanosHarion.Core.Roles.Thanos;

namespace ThanosHarion.Core.Buttons {

    [RegisterCooldownButton]
    public class SpaceButton : CustomButton<SpaceButton> {

        public override void OnCreateButton() {
            Timer = ThanosRoles.CooldownSpaceStone.GetValue();
            MaxTimer = ThanosRoles.CooldownSpaceStone.GetValue();
            UseNumber = 4;
            Roles = ThanosRoles.Instance;
            SetSprite("ThanosHarion.Resources.space.png", 300);
            DecreamteUseNimber = UseNumberDecremantion.OnClick;
            Key = ThanosRoles.KeyBindSpace.Key;
        }

        public override void OnClick() => VentUtils.PlaceVent(PlayerControl.LocalPlayer.transform.position);
    }
}