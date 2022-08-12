using BepInEx;
using HarmonyLib;
using UnityEngine;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Xml.Linq;
using MonoMod.Utils;

namespace ItemChoice
{
    public class _Assets
    {
        private static AssetBundle assetBundle;
        public static GameObject spiritObj;
        public static Mesh spiritMesh;
        public static Texture spiritTexture;
        public static Material whiteSpiritMat;
        public static Material blueSpiritMat;
        public static Material orangeSpiritMat;
        public static GameObject spiritInteract;
        public static GameObject itemChoiceUI;
        public static GameObject rowUI;
        public static GameObject spriteUI;


        public static void Init()
        {
            assetBundle = AssetBundle.LoadFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ItemChoice.__itemchoiceassets"));

            spiritObj = assetBundle.LoadAsset<GameObject>("spirit");
            spiritMesh = spiritObj.GetComponent<MeshFilter>().mesh;
            spiritTexture = assetBundle.LoadAsset<Texture2D>("spiritTexture");
            whiteSpiritMat = assetBundle.LoadAsset<Material>("White");
            blueSpiritMat = assetBundle.LoadAsset<Material>("Blue");
            orangeSpiritMat = assetBundle.LoadAsset<Material>("Orange");

            spiritInteract = assetBundle.LoadAsset<GameObject>("SpiritInteract");

            itemChoiceUI = assetBundle.LoadAsset<GameObject>("ItemChoiceUI");
            spriteUI = assetBundle.LoadAsset<GameObject>("Sprite");
            rowUI = assetBundle.LoadAsset<GameObject>("Row");
        }
    }

}
