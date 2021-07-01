using Harion.Cooldown;
using ThanosRoles = ThanosHarion.Core.Roles.Thanos;
using TimeCore = ThanosHarion.Core.System.Time.Time;

namespace ThanosHarion.Core.Buttons {

    [RegisterCooldownButton]
    public class TimeButton : CustomButton<TimeButton> {
        public override void OnCreateButton() {
            Timer = ThanosRoles.CooldownTimeStone.GetValue();
            Roles = ThanosRoles.Instance;
            EffectDuration = ThanosRoles.DurationTimeStone.GetValue() / 2;
            HasEffectDuration = true;
            SetSprite("ThanosHarion.Resources.time.png", 300);
            Key = ThanosRoles.KeyBindTime.Key;
        }

        public override void OnClick() => TimeCore.StartRewind();

        public override void OnEffectEnd() => TimeCore.StopRewind();

        public override void OnUpdate() {
            if (TimeCore.isRewinding)
                for (int i = 0; i < 2; i++)
                    TimeCore.Rewind();
            else
                TimeCore.Record();
        }
    }
}
