using Harion.Reactor;
using Harion.Utility.Helper;
using Harion.Utility.Utils;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace ThanosHarion {
    public static class ResourceLoader {
        private static readonly Assembly myAsembly = Assembly.GetExecutingAssembly();
        public static Sprite RealityStoneSprite;
        public static Sprite SpaceStoneSprite;
        public static Sprite SoulStoneSprite;
        public static Sprite MindStoneSprite;
        public static Sprite TimeStoneSprite;
        public static Sprite PowerStoneSprite;
        public static Sprite SnapSprite;
        public static Sprite PortalSprite;

        public static AnimationClip RealityAnimation;
        public static AnimationClip MindAnimation;
        public static AnimationClip PickupGemAnimation;
        public static AnimationClip PowerAnimation;

        public static AudioClip MindAudio;
        public static AudioClip RealityAudio;
        public static AudioClip PortalChangeAudio;
        public static AudioClip PowerAudio;
        public static AudioClip PickupGemAudio;

        public static void LoadAssets() {
            Stream resourceSteam = myAsembly.GetManifestResourceStream("ThanosHarion.Resources.thanos-harion");
            AssetBundle assetBundle = AssetBundle.LoadFromMemory(resourceSteam.ReadFully());

            RealityStoneSprite = SpriteHelper.LoadSpriteFromEmbeddedResources("ThanosHarion.Resources.reality.png", 300f).DontDestroy();
            SpaceStoneSprite = SpriteHelper.LoadSpriteFromEmbeddedResources("ThanosHarion.Resources.space.png", 300f).DontDestroy();
            SoulStoneSprite = SpriteHelper.LoadSpriteFromEmbeddedResources("ThanosHarion.Resources.soul.png", 300f).DontDestroy();
            MindStoneSprite = SpriteHelper.LoadSpriteFromEmbeddedResources("ThanosHarion.Resources.mind.png", 300f).DontDestroy();
            TimeStoneSprite = SpriteHelper.LoadSpriteFromEmbeddedResources("ThanosHarion.Resources.time.png", 300f).DontDestroy();
            PowerStoneSprite = SpriteHelper.LoadSpriteFromEmbeddedResources("ThanosHarion.Resources.power.png", 300f).DontDestroy();
            SnapSprite = SpriteHelper.LoadSpriteFromEmbeddedResources("ThanosHarion.Resources.snap.png", 450f).DontDestroy();
            PortalSprite = assetBundle.LoadAsset<Sprite>("Portal").DontDestroy();

            RealityAnimation = assetBundle.LoadAsset<AnimationClip>("AnimationReality.anim").DontDestroy();
            MindAnimation = assetBundle.LoadAsset<AnimationClip>("MindAnimation.anim").DontDestroy();
            PickupGemAnimation = assetBundle.LoadAsset<AnimationClip>("PickupGem.anim").DontDestroy();
            PowerAnimation = assetBundle.LoadAsset<AnimationClip>("Power.anim").DontDestroy();

            PickupGemAudio = assetBundle.LoadAsset<AudioClip>("PickupGem").DontDestroy();
            PortalChangeAudio = assetBundle.LoadAsset<AudioClip>("Portal Change").DontDestroy();
            PowerAudio = assetBundle.LoadAsset<AudioClip>("Power").DontDestroy();
            RealityAudio = assetBundle.LoadAsset<AudioClip>("Reality").DontDestroy();
            MindAudio = assetBundle.LoadAsset<AudioClip>("Mind").DontDestroy();
        }
    }
}
