using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ItemChoice
{
    public class ItemChoiceUI : MonoBehaviour
    {
        public static GameObject instance;
        public static int ItemChoiceUIID;
        public static int contentID;
        public static int targetObjID;
       
        public static void Init(Transform p)
        {
            instance = GameObject.Instantiate(_Assets.itemChoiceUI);
            instance.SetActive(false);
            instance.transform.SetParent(p);
            instance.transform.localPosition = new Vector3(0, 0, 0);
            ItemChoiceUIID = instance.GetInstanceID();
            contentID = ((GameObject)FindObjectFromInstanceID(ItemChoiceUIID)).transform.Find("VerticalScroll/VerticalViewport/VerticalContent").gameObject.GetInstanceID();
            AddPowerupsToUI(ItemManager.Instance.powerupsWhite);
            AddPowerupsToUI(ItemManager.Instance.powerupsBlue);
            AddPowerupsToUI(ItemManager.Instance.powerupsOrange);
        }

        public static void AddPowerupsToUI(Powerup[] powerUpsArray)
        {
            foreach (Powerup p in powerUpsArray)
            {
                if (ItemSpirit.isSpirit(p)) continue;
                GameObject holder = Instantiate(_Assets.powerupHolder);
                GameObject sprite = holder.transform.GetChild(0).gameObject;
                holder.SetActive(false);
                holder.name = p.name;
                holder.GetComponent<RectTransform>().SetParent(((GameObject)FindObjectFromInstanceID(contentID)).GetComponent<RectTransform>());
                holder.AddComponent<PowerupInItemChoiceUI>().powerup = p;
                sprite.GetComponent<RawImage>().texture = p.sprite.texture;
                sprite.GetComponent<Button>().onClick.AddListener(delegate { SpriteOnClick(p.id); });
            }
        }

        public static void ShowTierInUI(Powerup.PowerTier t)
        {
            foreach (Transform child in ((GameObject)FindObjectFromInstanceID(contentID)).GetComponent<RectTransform>())
            {
                if (!child.GetComponent<PowerupInItemChoiceUI>().powerup.tier.Equals(t))
                {
                    child.gameObject.SetActive(false);
                    continue;
                };
                child.gameObject.SetActive(true);
            }
        }

        public static void SpriteOnClick(int powerupID) // what happens when player clicks on item-choosing ui
        {
            HideUI();
            SpiritInteract.Instance.ChoseItem(powerupID, targetObjID);
        }

        public static void ToggleUI()
        {
            if (instance.activeInHierarchy)//Turn off the UI
            {
                instance.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            } else
            {
                instance.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        public static void HideUI()
        {
            if (instance.activeInHierarchy)//Turn off the UI
            {
                instance.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

    }

}
