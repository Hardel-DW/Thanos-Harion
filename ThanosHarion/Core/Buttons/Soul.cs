using Harion.Cooldown;
using Harion.Utility.Utils;
using ThanosRoles = ThanosHarion.Core.Roles.Thanos;

namespace ThanosHarion.Core.Buttons {
    [RegisterCooldownButton]
    public class SoulButton : CustomButton<SoulButton> {

        public override void OnCreateButton() {
            Timer = 1f;
            MaxTimer = 1f;
            Roles = ThanosRoles.Instance;
            SetSprite(ResourceLoader.SoulStoneSprite);
            PositionOffset = new UnityEngine.Vector2(0f, 0f);
            CustomKeyBind = () => ThanosRoles.KeyBindSoul.Key;
        }

        public override void OnUpdate() {
            StoneInformation SoulStone = StoneInformation.GetStoneData(StoneData.Soul);
            CanUse = SoulStone.HasStone && SoulStone.IsActive && GameUtils.GameStarted;
        }
    }
}
