using Harion.Cooldown;
using Harion.Utility.Utils;
using Hazel;
using ThanosRoles = ThanosHarion.Core.Roles.Thanos;

namespace ThanosHarion.Core.Buttons {

    [RegisterCooldownButton]
    public class PowerButton : CustomButton<PowerButton> {

        public override void OnCreateButton() {
            Timer = ThanosRoles.CooldownPowerStone.GetValue();
            MaxTimer = ThanosRoles.CooldownPowerStone.GetValue();
            Roles = ThanosRoles.Instance;
            SetSprite(ResourceLoader.PowerStoneSprite);
            PositionOffset = new UnityEngine.Vector2(1f, 1f);
            CustomKeyBind = () => ThanosRoles.KeyBindPower.Key;
        }

        public override void OnUpdate() {
            StoneInformation PowerStone = StoneInformation.GetStoneData(StoneData.Power);
            if (!PowerStone.HasStone && PowerStone.IsActive)
                CanUse = false;
        }

        public override void OnClick() {
            PlayerControlUtils.KillPlayerArea(PlayerControl.LocalPlayer.GetTruePosition(), PlayerControl.LocalPlayer, 2f);
            SendRpc();
        }

        public override void SendData(MessageWriter messageWriter) {
            messageWriter.WriteVector3(PlayerControl.LocalPlayer.transform.localPosition);
            SoundManager.Instance.PlaySound(ResourceLoader.PowerAudio, false, 100f);
            SpriteAnimUtils.StartAnimation(ResourceLoader.PowerAnimation, PlayerControl.LocalPlayer.transform.localPosition, 7.5f, 0.5f);
        }

        public override void ReadData(MessageReader messageReader) {
            messageReader.ReadListVector3();
            SpriteAnimUtils.StartAnimation(ResourceLoader.PowerAnimation, PlayerControl.LocalPlayer.transform.localPosition, 7.5f, 0.5f);
        }
    }
}
