using Harion.Cooldown;
using ThanosRoles = ThanosHarion.Core.Roles.Thanos;
using TimeCore = ThanosHarion.Core.System.Time.Time;

namespace ThanosHarion.Core.Buttons {

    [RegisterCooldownButton]
    public class TimeButton : CustomButton<TimeButton> {

        public override void OnCreateButton() {
            Timer = ThanosRoles.CooldownTimeStone.GetValue();
            MaxTimer = ThanosRoles.CooldownTimeStone.GetValue();
            EffectDuration = ThanosRoles.DurationTimeStone.GetValue() / 2;
            Roles = ThanosRoles.Instance;
            UseNumber = 4;
            HasEffectDuration = true;
            SetSprite(ResourceLoader.TimeStoneSprite);
            PositionOffset = new UnityEngine.Vector2(0f, 2f);
            CustomKeyBind = () => ThanosRoles.KeyBindTime.Key;
            DecreamteUseNumber = UseNumberDecremantion.OnClick;
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
