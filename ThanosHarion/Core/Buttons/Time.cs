using Harion.Cooldown;
using ThanosRoles = ThanosHarion.Core.Roles.Thanos;
using TimeCore = ThanosHarion.Core.System.Time.Time;

namespace ThanosHarion.Core.Buttons {

    [RegisterCooldownButton]
    public class TimeButton : CustomButton<TimeButton> {

        public override void OnCreateButton() {
            Timer = ThanosRoles.CooldownTimeStone.GetValue();
            Roles = ThanosRoles.Instance;
            UseNumber = 4;
            EffectDuration = ThanosRoles.DurationTimeStone.GetValue() / 2;
            HasEffectDuration = true;
            SetSprite(ResourceLoader.TimeStoneSprite);
            PositionOffset = new UnityEngine.Vector2(0f, 2f);
            CustomKeyBind = () => ThanosRoles.KeyBindTime.Key;
        }

        public override void OnClick() { 
            TimeCore.StartRewind();
            if (UseNumber == 0)
                UseNumber = int.MaxValue;
        }

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

            IsDisable = UseNumber == int.MaxValue;
        }
    }
}
