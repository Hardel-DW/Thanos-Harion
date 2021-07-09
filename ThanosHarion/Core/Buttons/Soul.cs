﻿using Harion.Cooldown;
using ThanosRoles = ThanosHarion.Core.Roles.Thanos;

namespace ThanosHarion.Core.Buttons {
    [RegisterCooldownButton]
    public class SoulButton : CustomButton<SoulButton> {

        public override void OnCreateButton() {
            Timer = 1f;
            MaxTimer = 1f;
            Roles = ThanosRoles.Instance;
            SetSprite(ResourceLoader.SoulStoneSprite);
            Key = ThanosRoles.KeyBindSoul.Key;
            PositionOffset = new UnityEngine.Vector2(0f, 0f);
        }

        public override void OnUpdate() {
            StoneInformation SoulStone = StoneInformation.GetStoneData(StoneData.Soul);
            if (!SoulStone.HasStone && SoulStone.IsActive)
                CanUse = false;
        }
    }
}
