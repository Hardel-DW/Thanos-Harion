using Harion.Cooldown;
using ThanosRoles = ThanosHarion.Core.Roles.Thanos;
using Harion.Utility;
using System.Collections.Generic;
using Harion.Utility.Ability;

namespace ThanosHarion.Core.Buttons {
    [RegisterCooldownButton]
    public class MindButton : CustomButton<MindButton> {

        public override void OnCreateButton() {
            Timer = ThanosRoles.CooldownMindStone.GetValue();
            MaxTimer = ThanosRoles.CooldownMindStone.GetValue();
            EffectDuration = ThanosRoles.DurationMindStone.GetValue();
            HasEffectDuration = true;
            Roles = ThanosRoles.Instance;
            SetSprite(ResourceLoader.MindStoneSprite);
            Key = ThanosRoles.KeyBindMind.Key;
            PositionOffset = new UnityEngine.Vector2(1f, 2f);
        }

        public override void OnClick() {
            PlayerButton.InitPlayerButton(
                false,
                new List<PlayerControl> { PlayerControl.LocalPlayer },
                (Player) => OnPlayerChoose(Player),
                () => PlayerButton.StopPlayerSelection()
            );
        }

        public override void OnEffectEnd() => Morphing.Unmorph(PlayerControl.LocalPlayer, true);

        public override void OnUpdate() {
            StoneInformation MindStone = StoneInformation.GetStoneData(StoneData.Mind);
            if (!MindStone.HasStone && MindStone.IsActive)
                CanUse = false;
        }

        private void OnPlayerChoose(PlayerControl Player) {
            Morphing.Morph(PlayerControl.LocalPlayer, Player, true);
        }
    }
}
