using Harion.ModsManagers;
using Harion.ModsManagers.Configuration;
using Harion.ModsManagers.Mods;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ThanosHarion {
    public class ModManager : ModRegistry, IModManager, IModManagerUpdater, IModManagerLink {

        public string DisplayName => "Thanos Harion";
        public string Version => "V" + typeof(ThanosHarionPlugin).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
        public string Description => "Add the Thanos role with the 6 affinity stones to find and hide in the map, each stone offers powers to unlock the Snap and finish the game.";
        public string Credit => "Thanos created by Hardel";
        public string SmallDescription => "Add Thanos in Among us.";
        public string GithubRepositoryName => "Thanos-Harion";
        public string GithubAuthorName => "Hardel-DW";
        public GithubVisibility GithubRepositoryVisibility => GithubVisibility.Public;
        public string GithubAccessToken => "";
        public Dictionary<string, Sprite> ModsLinks => new Dictionary<string, Sprite>() {
            { "https://www.patreon.com/hardel", ModsSocial.PatreonSprite },
            { "https://discord.gg/HZtCDK3s",  ModsSocial.GithubSprite }
        };
    }
}
