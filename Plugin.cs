using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using makeMedsGreatAgain.MyPatches;

namespace makeMedsGreatAgain
{
    // first string below is your plugin's GUID, it MUST be unique to any other mod. Read more about it in BepInEx docs. Be sure to update it if you copy this project.
    [BepInPlugin("com.vinihns.makeMedsGreatAgain", "Make Meds Great Again!", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource LogSource;
        public static ConfigEntry<bool> canWalkInSurgery;

        // BaseUnityPlugin inherits MonoBehaviour, so you can use base unity functions like Awake() and Update()
        private void Awake()
        {
            // save the Logger to variable so we can use it elsewhere in the project
            LogSource = Logger;
            LogSource.LogInfo("Adding little things");

            canWalkInSurgery = Config.Bind("Config", "Can walk in surgery", true, "Boolean setting description");
            
            // uncomment line(s) below to enable desired example patch, then press F6 to build the project:
            new SimplePatch().Enable();
        }
    }
}
