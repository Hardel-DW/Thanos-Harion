using Harion.Reactor;
using Harion.Utility.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace ThanosHarion {
    public static class ResourceLoader {
        private static readonly Assembly MyAssembly = Assembly.GetExecutingAssembly();
       /* public static GameObject MyGameObject;
        public static Sprite MySprite;*/

        public static void LoadAssets() {
            /* Stream resourceSteam = MyAssembly.GetManifestResourceStream("ThanosHarion.Resources.myBundle");
             AssetBundle assetBundle = AssetBundle.LoadFromMemory(resourceSteam.ReadFully());

             MyGameObject = assetBundle.LoadAsset<GameObject>("MyGameObject.prefab").DontDestroy();
             MySprite = assetBundle.LoadAsset<Sprite>("MySprite").DontDestroy();*/
        }
    }
}
