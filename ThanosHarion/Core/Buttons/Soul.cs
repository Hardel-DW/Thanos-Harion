using Harion.Cooldown;
using ThanosRoles = ThanosHarion.Core.Roles.Thanos;

namespace ThanosHarion.Core.Buttons {
    [RegisterCooldownButton]
    public class SoulButton : CustomButton<SoulButton> {

        public bool HasStone = false;

        public override void OnCreateButton() {
            Timer = 1f;
            MaxTimer = 1f;
            Roles = ThanosRoles.Instance;
            SetSprite("ThanosHarion.Resources.soul.png", 300);
            Key = ThanosRoles.KeyBindSoul.Key;
            PositionOffset = new UnityEngine.Vector2(0f, 0f);
        }

        public override void OnUpdate() {
            if (!HasStone)
                CanUse = false;
        }
    }
}
