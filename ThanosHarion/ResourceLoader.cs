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
        }
    }
}
