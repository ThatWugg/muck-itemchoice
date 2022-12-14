using System;
using System.Collections.Generic;
using System.Net.Sockets;
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
            bool hasSpiritInteract = false;
            foreach(Transform child in ItemManager.Instance.list[parentID].GetComponent<Transform>())
            {
                if (!child.name.Equals("SpiritInteract")) continue;
                hasSpiritInteract = true;
            }
            if (!hasSpiritInteract) return;
            var tier = ItemManager.Instance.list[parentID].GetComponent<Item>().powerup.tier;
            ItemChoiceUI.targetObjID = parentID;
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

        public void ChoseItem(int powerupID, int targetID)
        {
            GameObject changeObj = ItemManager.Instance.list[targetID];
            Powerup toPowerup = ItemManager.Instance.allPowerups[powerupID];
            RemoveAndDropPowerup(toPowerup.id, changeObj.transform.position, targetID);
        }

        public void RemoveAndDropPowerup(int powerupID, Vector3 pos, int id)
        {
            ItemManager.Instance.PickupItem(id);//Copy and pasted...
            ServerSend.PickupItem(-1, id);
            // Client sided code below, this means other players will still see a spirit object when a client has picked a powerup
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
            // Client sided code end
            ServerSend.DropPowerupAtPosition(powerupID, id, pos); // Tells the server to drop a powerup at that spot
        }
    }

}
