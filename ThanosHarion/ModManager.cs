using Harion.ModsManagers;
using Harion.ModsManagers.Configuration;
using Harion.ModsManagers.Mods;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ThanosHarion {
    public class ModManager : ModRegistry, IModManager, IModManagerUpdater, IModManagerLink {

        /**
         * The name of your mod that will be displayed in the Mod Manager.
         */
        public string DisplayName => "Harion Template";

        /**
         * The version of your Mod.
         * This line is a bit complex, it simply allows you to get the version of the Csproj file.
         * For the automatic update of this mod works it is necessary that the content of this variable is identical to the Tag in the Github realease.
         */
        public string Version => "V" + typeof(TemplatePlugin).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

        /**
         * The description of your mod that will be displayed in the Mod Manager.
         */
        public string Description => "I don't know what to put as a description, you can just set a description of your mod here, It will be visible in the ModManager.";

        /**
         * For the moment this data is not used.
         */
        public string Credit => "Template created by Hardel";

        /**
         * The small description of your mod, when you are at the mod selection in the ModManager.
         */
        public string SmallDescription => "This is a example small description";

        /**
         * For the link with your Github you must indicate the Repository Name 
         */
        public string GithubRepositoryName => "ThanosHarion";

        /**
         * For the link with your Github you must indicate the Authour Name of Repository
         */
        public string GithubAuthorName => "Hardel-DW";

        /**
         * Indicate the visibility of the Github project.
         */
        public GithubVisibility GithubRepositoryVisibility => GithubVisibility.Public;

        /**
         * Only if the Github is private, Indicate the token to access the Github api.
         * 
         * How to create token :
         * 1) Connect to Github website and connect you.
         * 2) Go to the settings
         * 3) Then go to the Developer settings, on the left side.
         * 4) Go to Personal access tokens.
         * 5) Generate new Token
         * 
         * Warning:
         * Only permission to access the Release is required, do not give any other permission.
         * Do not communicate this token publicly.
         * If the mod must be public, delete the token.
         * The token can be obtained if you have the dll file, use this feature only with trusted people.
         */
        public string GithubAccessToken => "";

        /**
         * Social network links.
         * Simply on the left side the link, And on the right side the sprite.
         * Some sprite are included in Harion on ModsSocial class.
         * 
         * The size should be 512 by 512 in 100 pixel per unit.
         * You can use either the ResourceLoader to load the Sprite.
         * Either by dropping your image in the Resource folder and use this line to load Sprite.
         * SpriteHelper.LoadSpriteFromEmbeddedResources("ThanosHarion.Resources.MyImage.png", 100f).DontDestroy()
         * 
         * Note:
         * If IModManagerUpdater is implemented, a social link to the Github will be automatically created.
         */
        public Dictionary<string, Sprite> ModsLinks => new Dictionary<string, Sprite>() {
            { "https://www.patreon.com/hardel", ModsSocial.PatreonSprite },
            { "https://discord.gg/HZtCDK3s",  ModsSocial.GithubSprite }
        };
    }
}
