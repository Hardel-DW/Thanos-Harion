using Harion.Cooldown;
using Harion.Utility.Utils;
using Hazel;
using System.Linq;
using UnityEngine;
using ThanosRoles = ThanosHarion.Core.Roles.Thanos;

namespace ThanosHarion.Core.Buttons {
    [RegisterCooldownButton]
    public class SnapButton : CustomButton<SnapButton> {
        private bool SnapUsed = false;

        public override void OnCreateButton() {
            Timer = 30f;
            MaxTimer = 30f;
            EffectDuration = 5f;
            HasEffectDuration = true;
            Roles = ThanosRoles.Instance;
            SetSprite(ResourceLoader.SnapSprite);
            Key = ThanosRoles.KeyBindSnap.Key;
            PositionOffset = new Vector2(0.5f, 3f);

        }

        public override void OnClick() {
            MessageWriter write = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.Snap, SendOption.None, -1);
            write.Write(PlayerControl.LocalPlayer.PlayerId);
            AmongUsClient.Instance.FinishRpcImmediately(write);
            StartSnap();
        }

        public override void OnEffectEnd() => SnapEnd();

        public override void OnUpdate() {
            if (SnapUsed)
                Increment();

            if (SnapUsed && DestroyableSingleton<HudManager>.Instance.FullScreen.color.a >= 1)
                SnapEnd();

            if (StoneInformation.StonesData.Any(stone => !stone.HasStone && stone.IsActive))
                CanUse = false;
        }

        private void Increment() {
            Camera.main.GetComponent<FollowerCamera>().shakeAmount = 0.3f;
            Camera.main.GetComponent<FollowerCamera>().shakePeriod = 600f;

            Color currentColor = DestroyableSingleton<HudManager>.Instance.FullScreen.color;
            DestroyableSingleton<HudManager>.Instance.FullScreen.enabled = true;
            DestroyableSingleton<HudManager>.Instance.FullScreen.color = new Color(1f, 1f, 1f, currentColor.a + 0.0025f);
        }

        public void StartSnap() {
            SnapUsed = true;
            Camera.main.GetComponent<FollowerCamera>().shakeAmount = 0.3f;
            Camera.main.GetComponent<FollowerCamera>().shakePeriod = 600f;
            DestroyableSingleton<HudManager>.Instance.FullScreen.enabled = true;
            DestroyableSingleton<HudManager>.Instance.FullScreen.color = new Color(1f, 1f, 1f, 0f);
        }

        private void SnapEnd() {
            SnapUsed = false;
            Camera.main.GetComponent<FollowerCamera>().shakeAmount = 0f;
            Camera.main.GetComponent<FollowerCamera>().shakePeriod = 0f;

            DestroyableSingleton<HudManager>.Instance.FullScreen.color = new Color(1f, 1f, 1f, 0f);
            DestroyableSingleton<HudManager>.Instance.FullScreen.enabled = false;

            PlayerControlUtils.KillEveryone(PlayerControl.LocalPlayer);
        }
    }
}
