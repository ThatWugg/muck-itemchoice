using System.Diagnostics;
using System;
using UnityEngine;

namespace ItemChoice
{
    public class SpiritInteract : MonoBehaviour, Interactable
    {
        public int parentID;
        public static SpiritInteract Instance;

        public void Awake()
        {
            Instance = this;
        }
        public void AllExecute()
        {
        }

        public string GetName()
        {
            return "Choose item";
        }

        public void Interact()
        {
            var tier = ItemManager.Instance.list[parentID].GetComponent<Item>().powerup.tier;
            ItemChoiceUI.ShowTierInUI(tier);
            ItemChoiceUI.ToggleUI();
        }

        public bool IsStarted()
        {
            return false;
        }

        public void LocalExecute()
        {
        }

        public void RemoveObject()
        {

        }

        public void ServerExecute(int fromClient = -1)
        {

        }

        public void ChoseItem(int powerupID)
        {
            GameObject changeObj = ItemManager.Instance.list[parentID];
            Powerup toPowerup = ItemManager.Instance.allPowerups[powerupID];
            foreach (Transform child in changeObj.transform)
            {
                if (!(child.name.Equals("SpiritInteract") || child.name.Equals("PowerupParticles(Clone)"))) continue;
                Destroy(child.gameObject);
            }
            ItemChoiceUI.ToggleUI();
            RemoveAndDropPowerup(toPowerup.id, changeObj.transform.position, parentID);
            ItemChoiceUI.HideUI();
        }

        public void RemoveAndDropPowerup(int powerupID, Vector3 pos, int id)
        {
            ItemManager.Instance.PickupItem(id);//Copy and pasted...
            ServerSend.PickupItem(-1, id);

            GameObject gameObject = GameObject.Instantiate(ItemManager.Instance.dropItem); //This is just a copy and paste from the DropPowerUpAtPosition from ItemManager...
            Powerup powerup = GameObject.Instantiate(ItemManager.Instance.allPowerups[powerupID]);
            Item component = gameObject.GetComponent<Item>();
            component.powerup = powerup;
            component.UpdateMesh();
            gameObject.AddComponent<BoxCollider>();
            gameObject.transform.position = pos;
            if (ItemManager.Instance.attatchDebug)
            {
                GameObject obj = GameObject.Instantiate(ItemManager.Instance.debug, gameObject.transform);
                obj.GetComponent<DebugObject>().text = string.Concat(id);
                obj.transform.localPosition = Vector3.up * 1.25f;
            }
            gameObject.GetComponent<Item>().objectID = id;
            gameObject.GetComponent<Item>().pickupDelay = 0.5f;
            ItemManager.Instance.list.Add(id, gameObject);
            ServerSend.DropPowerupAtPosition(powerupID, id, pos);
        }
    }

}
