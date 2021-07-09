using Harion.Cooldown;
using Harion.Utility.Utils;
using ThanosRoles = ThanosHarion.Core.Roles.Thanos;

namespace ThanosHarion.Core.Buttons {

    [RegisterCooldownButton]
    public class PowerButton : CustomButton<PowerButton> {

        public override void OnCreateButton() {
            Timer = ThanosRoles.CooldownPowerStone.GetValue();
            MaxTimer = ThanosRoles.CooldownPowerStone.GetValue();
            Roles = ThanosRoles.Instance;
            SetSprite(ResourceLoader.PowerStoneSprite);
            Key = ThanosRoles.KeyBindPower.Key;
            PositionOffset = new UnityEngine.Vector2(1f, 1f);
        }

        public override void OnUpdate() {
            StoneInformation PowerStone = StoneInformation.GetStoneData(StoneData.Power);
            if (!PowerStone.HasStone && PowerStone.IsActive)
                CanUse = false;
        }

        public override void OnClick() => PlayerControlUtils.KillPlayerArea(PlayerControl.LocalPlayer.GetTruePosition(), PlayerControl.LocalPlayer, 1f);
    }
}
