using Harion.Reactor;
using Harion.Utility.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

/**
 * Description :
 * The AssetsBundle, allow to load Unity contents like Prefab, Sprite, Audio, AnimationClip and others....
 * 
 * Install AssetBundles-Browser :
 * 1) To create one you just need to install Unity 2020.2.6f1 specifically for this version.
 * 2) Create a 3D or 2D project.
 * 3) Open Unity Package Manager
 * 4) Click the + (Add) button at the top, left corner of the window and choose Add package from git URL…
 * 5) Enter this link : https://github.com/Unity-Technologies/AssetBundles-Browser.
 * 6) Now you just have to add an AssetBundle for your elements, and press Build in Window > AssetBundle Browser.
 * 
 * On your mod:
 * Add your AssetBundle in the "Resource" folder so that it is considered as Embedded Resource.
 * And at the bottom don't forget to indicate the path to the AssetBundle.
 * To import elements, there are examples below.
 */

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
