using BepInEx;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace ItemChoice
{

    public class ItemSpirit
    {

        public static Powerup whiteSpirit = ScriptableObject.CreateInstance<Powerup>();
        public static Powerup blueSpirit = ScriptableObject.CreateInstance<Powerup>();
        public static Powerup orangeSpirit = ScriptableObject.CreateInstance<Powerup>();
        public static Powerup[] spiritList = { whiteSpirit, blueSpirit, orangeSpirit };
        public static int[] spiritID = { 0, 0, 0 }; 

        public static void Init()
        {

            whiteSpirit.name = "White Spirit";
            whiteSpirit.tier = Powerup.PowerTier.White;
            whiteSpirit.description = "A spirit of a white powerup... it was everything but now it is nothing";
            whiteSpirit.mesh = _Assets.spiritMesh;
            whiteSpirit.material = _Assets.whiteSpiritMat;

            blueSpirit.name = "Blue Spirit";
            blueSpirit.tier = Powerup.PowerTier.Blue;
            blueSpirit.description = "A spirit of a blue powerup... it was everything but now it is nothing";
            blueSpirit.mesh = _Assets.spiritMesh;
            blueSpirit.material = _Assets.blueSpiritMat;

            orangeSpirit.name = "Orange Spirit";
            orangeSpirit.tier = Powerup.PowerTier.Orange;
            orangeSpirit.description = "A spirit of a orange powerup... it was everything but now it is nothing";
            orangeSpirit.mesh = _Assets.spiritMesh;
            orangeSpirit.material = _Assets.orangeSpiritMat;
        }

        public static bool isSpirit(Powerup testMe)
        {
            foreach (Powerup powerup in spiritList)
            {
                if (powerup.name.Equals(testMe.name))
                {
                    return true;
                }
            }
            return false;
        }

        public static void initSpiritID(Dictionary<int, Powerup> allPowerups)
        {
            int i = 0;
            foreach(KeyValuePair<int, Powerup> powerup in allPowerups)
            {
                if (isSpirit(powerup.Value))
                {
                    spiritID[i] = powerup.Key;
                    i++;
                }
            }
        }
    }
}
