using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ItemChoice
{
    public class ItemChoiceUI : MonoBehaviour
    {
        public static GameObject instance;
        public static ItemChoiceUI actuallyTheInstance;
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

        public static void ShowTierInUI(Powerup.PowerTier tier)
        {
            foreach (Transform obj in ((GameObject)FindObjectFromInstanceID(contentID)).transform)
            {
                if (tier.ToString().Equals(obj.gameObject.name))
                {
                    obj.gameObject.SetActive(true);
                    continue;
                }
                obj.gameObject.SetActive(false);
            }
        }

        public static void AddPowerupsToUI(Powerup[] powerUpsArray)
        {
            Powerup[] powerupsSend = new Powerup[3];
            for (int i = 0;i < powerUpsArray.Length; i++)
            {
                if (ItemSpirit.isSpirit(powerUpsArray[i])) continue;
                powerupsSend[i % 3] = powerUpsArray[i];
                if (powerupsSend[2] != null)
                {
                    AddRow(powerupsSend, powerUpsArray[0].tier.ToString());
                    powerupsSend = new Powerup[3];
                }
            }
            if (powerupsSend[0] != null)
            {
                AddRow(powerupsSend, powerUpsArray[0].tier.ToString());
            }
        }

        public static GameObject AddRow(Powerup[] powerUpsArray, string rowName)
        {
            var row = GameObject.Instantiate(_Assets.rowUI);
            row.name = rowName;
            for (int i = 0; i < powerUpsArray.Length; i++)
            {
                AddSprite(row.GetInstanceID(), powerUpsArray[i]);
            }
            row.GetComponent<Transform>().SetParent(((GameObject)FindObjectFromInstanceID(contentID)).transform);
            row.transform.localScale = new Vector3(1, 1, 1);
            row.SetActive(false);
            return row;
        }

        public static GameObject AddSprite(int rowID, Powerup powerup)
        {
            if (!(bool)powerup) return null;
            var sprite = GameObject.Instantiate(_Assets.spriteUI);
            sprite.name = powerup.id.ToString();
            sprite.transform.SetParent(((GameObject)FindObjectFromInstanceID(rowID)).GetComponent<Transform>());
            sprite.GetComponent<RawImage>().texture = powerup.sprite.texture;
            sprite.GetComponent<Button>().onClick.AddListener(delegate { SpiritInteract.Instance.ChoseItem(powerup.id, targetObjID); });
            return sprite;
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
