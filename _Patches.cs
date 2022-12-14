using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ItemChoice
{
    [HarmonyPatch]
    public class _Patches
    {
        [HarmonyPrefix] //Register the white, blue, and orange spirits as powersups 
        [HarmonyPatch(typeof(ItemManager), "InitAllPowerups")]
        private static void RegisterPowerups(ref Powerup[] ___powerupsWhite, ref Powerup[] ___powerupsBlue, ref Powerup[] ___powerupsOrange)
        {
            ___powerupsWhite = ___powerupsWhite.AddToArray(ItemSpirit.whiteSpirit);

            ___powerupsBlue = ___powerupsBlue.AddToArray(ItemSpirit.blueSpirit);

            ___powerupsOrange = ___powerupsOrange.AddToArray(ItemSpirit.orangeSpirit);
        }

        [HarmonyPostfix] // saves the ids of the spirits when they are initialized
        [HarmonyPatch(typeof(ItemManager), "InitAllPowerups")]
         private static void RegisterID(Dictionary<int, Powerup> ___allPowerups)
        {
            ItemSpirit.initSpiritID(___allPowerups);
        }

        [HarmonyPrefix] // Changes all powerups to the spirits of corresponding tiers for multiplayer
        [HarmonyPatch(typeof(ItemManager), "DropPowerupAtPosition")]
        public static void DropPowerupAtPosition(ref int powerupId, Dictionary<int, Powerup> ___allPowerups)
        {
            switch (___allPowerups[powerupId].tier)
            {
                case Powerup.PowerTier.White:
                    powerupId = ItemSpirit.spiritID[0];
                    break;
                case Powerup.PowerTier.Blue:
                    powerupId = ItemSpirit.spiritID[1];
                    break;
                case Powerup.PowerTier.Orange:
                    powerupId = ItemSpirit.spiritID[2];
                    break;
            }
        }

        [HarmonyPostfix] // Adds the SpiritInteract component to dropped spirits
        [HarmonyPatch(typeof(ItemManager), "DropPowerupAtPosition")]
        public static void AddInteractComponent(ref int powerupId, Dictionary<int, Powerup> ___allPowerups, int objectID, ref Dictionary<int, GameObject> ___list)
        {
            Transform powerupTransform = ___list[objectID].transform;
            if (!ItemSpirit.isSpirit(___allPowerups[powerupId])) return;
            GameObject interactObj = UnityEngine.Object.Instantiate(_Assets.spiritInteract, powerupTransform.position, powerupTransform.rotation);
            interactObj.name = "SpiritInteract";
            interactObj.layer = 15;
            interactObj.AddComponent<SpiritInteract>();
            GameObject.Destroy(interactObj.GetComponent<MeshRenderer>());   // Could be changed in assetbundle
            interactObj.GetComponent<SpiritInteract>().parentID = objectID;
            interactObj.GetComponent<SphereCollider>().isTrigger = true;
            interactObj.transform.parent = ___list[objectID].transform;

        }

        //Prevent picking up the spirits.
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Item), "ReadyToPickup")]
        private static void CancelPickup(Item __instance, ref bool ___readyToPickUp)
        {
            if (!(bool)__instance.powerup) return;
            if (!ItemSpirit.isSpirit(__instance.powerup)) return;
            ___readyToPickUp = false;
        }

        [HarmonyPostfix]//Add the ItemChoiceUI to UI (1)
        [HarmonyPatch(typeof(UiController), "Awake")]
        private static void Bruh(UiController ___Instance)
        {
            ItemChoiceUI.Init(___Instance.gameObject.transform);
        }

        [HarmonyPrefix] //Count powerup-choosing UI as a UI
        [HarmonyPatch(typeof(OtherInput), "OtherUiActive")]
        public static bool ItemChoiceUIActive(ref bool __result)
        {
            if (ItemChoiceUI.instance.activeInHierarchy) {
                __result = true;
                return false;
            }
            return true;
        }

        [HarmonyPrefix] //Close powerup-choosing UI when other UI are opened
        [HarmonyPatch(typeof(OtherInput), "Update")]
        private static bool ItemChoiceUICancel()
        {
            if (GameManager.state == GameManager.GameState.GameOver) return true;
            if (OtherInput.Instance.pauseUi.activeInHierarchy || OtherInput.Instance.settingsUi.activeInHierarchy) return true;
            if (Input.GetKeyDown(InputManager.map) || Input.GetKeyDown(InputManager.inventory))
            {
                ItemChoiceUI.HideUI();
                return true;
            }
            if (!(Input.GetButton("Cancel") && ItemChoiceUI.instance.activeInHierarchy)) return true;
            ItemChoiceUI.HideUI();
            return false;
        }

        [HarmonyPrefix] // Stopped hotbar scrolling
        [HarmonyPatch(typeof(Hotbar), "Update")]
        private static bool Update()
        {
            if (!ItemChoiceUI.instance.activeInHierarchy) return true;
            return false;
        }
    }

}
