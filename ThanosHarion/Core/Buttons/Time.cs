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
            SetSprite(ResourceLoader.TimeStoneSprite);
            Key = ThanosRoles.KeyBindTime.Key;
            PositionOffset = new UnityEngine.Vector2(0f, 2f);
        }

        public override void OnClick() => TimeCore.StartRewind();

        public override void OnEffectEnd() => TimeCore.StopRewind();

        public override void OnUpdate() {
            if (TimeCore.isRewinding)
                for (int i = 0; i < 2; i++)
                    TimeCore.Rewind();
            else
                TimeCore.Record();

            StoneInformation TimeStone = StoneInformation.GetStoneData(StoneData.Time);
            if (!TimeStone.HasStone && TimeStone.IsActive)
                CanUse = false;
        }
    }
}
