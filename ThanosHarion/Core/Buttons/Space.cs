using Harion.Utility.Utils;
using Harion.Cooldown;
using ThanosRoles = ThanosHarion.Core.Roles.Thanos;
using Hazel;
using UnityEngine;

namespace ThanosHarion.Core.Buttons {

    [RegisterCooldownButton]
    public class SpaceButton : CustomButton<SpaceButton> {

        public override void OnCreateButton() {
            Timer = ThanosRoles.CooldownSpaceStone.GetValue();
            MaxTimer = ThanosRoles.CooldownSpaceStone.GetValue();
            UseNumber = 4;
            Roles = ThanosRoles.Instance;
            SetSprite(ResourceLoader.SpaceStoneSprite);
            DecreamteUseNumber = UseNumberDecremantion.OnClick;
            PositionOffset = new Vector2(0f, 1f);
            CustomKeyBind = () => ThanosRoles.KeyBindSpace.Key;
        }

        public override void OnClick() {
            SendRpc();
        }

        public override void OnUpdate() {
            StoneInformation SpaceStone = StoneInformation.GetStoneData(StoneData.Space);
            if (!SpaceStone.HasStone && SpaceStone.IsActive)
                CanUse = false;
        }

        public override void SendData(MessageWriter messageWriter) {
            messageWriter.WriteVector3(PlayerControl.LocalPlayer.transform.position);
            VentUtils.PlaceLocalVent(PlayerControl.LocalPlayer.transform.position, ResourceLoader.PortalSprite);
        }

        public override void ReadData(MessageReader messageReader) {
            Vector3 Position = messageReader.ReadVector3();
            VentUtils.PlaceLocalVent(Position, ResourceLoader.PortalSprite);
        }
    }
}