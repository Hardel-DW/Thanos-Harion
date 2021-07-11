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
            DecreamteUseNimber = UseNumberDecremantion.OnClick;
            PositionOffset = new Vector2(0f, 1f);
            CustomKeyBind = () => ThanosRoles.KeyBindSpace.Key;
        }

        public override void OnClick() {
            SendRpc();
            if (UseNumber == 0)
                UseNumber = int.MaxValue;
        }

        public override void OnUpdate() {
            StoneInformation SpaceStone = StoneInformation.GetStoneData(StoneData.Space);
            if (!SpaceStone.HasStone && SpaceStone.IsActive)
                CanUse = false;
            
            IsDisable = UseNumber == int.MaxValue;
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