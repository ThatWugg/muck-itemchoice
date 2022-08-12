using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace ItemChoice
{
    [BepInPlugin(MOD_ID, MOD_NAME, MOD_VERSION)]
    public class _Main : BaseUnityPlugin
    {
        private const string MOD_ID = "wugg.itemchoice";
        private const string MOD_NAME = "Item Choice";
        private const string MOD_VERSION = "1.0.0.2";
        private Harmony harmony = new Harmony(MOD_ID);

        private void Awake()
        {
            _Assets.Init();

            ItemSpirit.Init();

            harmony.PatchAll();
        }
    }

}
