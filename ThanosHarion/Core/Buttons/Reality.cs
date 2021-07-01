using Harion.Cooldown;
using Harion.Utility.Ability;
using Harion.Utility.Utils;
using ThanosRoles = ThanosHarion.Core.Roles.Thanos;

namespace ThanosHarion.Core.Buttons {
    [RegisterCooldownButton]
    public class RealityButton : CustomButton<RealityButton> {
        public override void OnCreateButton() {
            Timer = ThanosRoles.CooldownRealityStone.GetValue();
            MaxTimer = ThanosRoles.CooldownRealityStone.GetValue();
            EffectDuration = ThanosRoles.DurationRealityStone.GetValue();
            HasEffectDuration = true;
            Roles = ThanosRoles.Instance;
            SetSprite("ThanosHarion.Resources.reality.png", 300);
            Key = ThanosRoles.KeyBindReality.Key;
            PositionOffset = new UnityEngine.Vector2(1f, 0f);
        }

        public override void OnClick() => Invisbility.LaunchInvisibility(PlayerControl.LocalPlayer, ThanosRoles.DurationRealityStone.GetValue(), PlayerControlUtils.GetImpostors());
    }
}
