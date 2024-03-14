using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public static int itemCounter = 1000;

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private float currenWeight;
    [SerializeField]
    private float maxWeight;

    [SerializeField]
    private Transform ItemContainer;

    public inventoryItem equippedSlot;

    public GameObject itemPrefab;


    [SerializeField]
    private int inventorySize;

    public List<inventoryItem> inventoryList = new List<inventoryItem>();

    public bool addItem(Item item)
    {
        if (item.itemWeight + updateWeight() != maxWeight && inventoryList.Count < inventorySize)
        {

            if (inventoryList.Find(inventoryItem => inventoryItem.itemName == item.itemName) == null || item.stackable == false)
            {
                inventoryList.Add(new inventoryItem(item.itemType, item.itemName, item.itemWeight, item.automatic, item.timetoFire, item.damage, item.spread, item.distance, item.magSize, item.heal, item.duration, itemCounter++, item.stackable, item.itemObject, item.currentMag, item.MuzzelPostion, item.audioClip, item.bullet, item.Muzzelflasch));
                return true;
            }
            else
            {
                inventoryList.Find(inventoryItem => inventoryItem.itemName == item.itemName).amount += 1;
                return true;
            }
        }
        return false;

    }

    public bool addItem(inventoryItem item, int amount)
    {
        if (item.itemWeight + updateWeight() != maxWeight && inventoryList.Count < inventorySize)
        {

            if (inventoryList.Find(inventoryItem => inventoryItem.itemName == item.itemName) == null || item.stackable == false)
            {
                inventoryList.Add(new inventoryItem(item.itemType, item.itemName, item.itemWeight, item.automatic, item.timetoFire, item.damage, item.spread, item.distance, item.magSize, item.heal, item.duration, item.itemID, item.stackable, item.itemObject, item.currentMag, item.MuzzelPos, item.audioClip, item.bullet, item.Muzzelflasch));
                return true;
            }
            else
            {
                inventoryList.Find(inventoryItem => inventoryItem.itemName == item.itemName).amount += 1;
                return true;
            }
        }
        return false;

    }

    public float updateWeight()
    {
        float weight = 0;

        for (int i = 0; i < inventoryList.Count; i++)
        {
            weight += inventoryList[i].itemWeight * inventoryList[i].amount;
        }

        return weight;
    }

    public void dropItem(inventoryItem item, int amount)
    {
        if (item.equipd == false)
        {
            for (int i = 0; i < inventoryList.Count; i++)
            {
                if (item.itemID == inventoryList[i].itemID && inventoryList[i].amount - amount > 0)
                {
                    createItem(item, amount);
                    inventoryList[i].amount -= amount;
                }
                else
                {
                    createItem(item, amount);
                    inventoryList.RemoveAt(i);
                }
            }
        }

    }

    public void removeItem(inventoryItem item, int amount)
    {
        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (item.itemID == inventoryList[i].itemID && inventoryList[i].amount - amount > 0)
            {
                inventoryList[i].amount -= amount;
            }
            else
            {
                inventoryList.RemoveAt(i);
            }
        }
    }

    public void removeItem(int id)
    {
        inventoryList.RemoveAt(inventoryList.FindIndex(inventoryItem => inventoryItem.itemID == id));
    }

    public void equipItem(inventoryItem item, int id)
    {

        if (item.itemType == ItemType.Weapon)
        {
            equippedSlot = inventoryList.Find(inventoryItem => inventoryItem.itemID == id);
            removeItem(id);
            if (equippedSlot != null)
            {
                GameObject.Find("WeaponHolder").GetComponent<GunManager>().gunEquippd = true;
            }
        }
        if (item.itemType == ItemType.Tool)
        {
            equippedSlot = inventoryList.Find(inventoryItem => inventoryItem.itemID == id);
            removeItem(id);
            if (equippedSlot != null)
            {
                GameObject.Find("FarmManager").GetComponent<Farming>().equipdTool = true;
            }
        }

    }

    public void unEquipItem()
    {
        if (equippedSlot != null)
        {
            inventoryList.Add(new inventoryItem(equippedSlot.itemType, equippedSlot.itemName, equippedSlot.itemWeight, equippedSlot.automatic, equippedSlot.timetoFire, equippedSlot.damage, equippedSlot.spread, equippedSlot.distance, equippedSlot.magSize, equippedSlot.heal, equippedSlot.duration, equippedSlot.itemID, equippedSlot.stackable, equippedSlot.itemObject, equippedSlot.currentMag, equippedSlot.MuzzelPos, equippedSlot.audioClip, equippedSlot.bullet, equippedSlot.Muzzelflasch));
            equippedSlot = null;
            GameObject.Find("WeaponHolder").GetComponent<GunManager>().gunEquippd = false;
            GameObject.Find("FarmManager").GetComponent<Farming>().equipdTool = false;
        }
    }

    public void createItem(inventoryItem item, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject newItem = Instantiate(itemPrefab, Player.transform.position, Quaternion.identity);
            newItem.transform.SetParent(ItemContainer);
            Item itemComponent = newItem.AddComponent<Item>();
            itemComponent.itemType = item.itemType;
            itemComponent.itemName = item.itemName;
            itemComponent.itemWeight = item.itemWeight;
            itemComponent.stackable = item.stackable;

            itemComponent.automatic = item.automatic;
            itemComponent.timetoFire = item.timetoFire;
            itemComponent.damage = item.damage;
            itemComponent.spread = item.spread;
            itemComponent.distance = item.distance;
            itemComponent.magSize = item.magSize;

            itemComponent.heal = item.heal;
            itemComponent.duration = item.duration;
            itemComponent.currentMag = item.currentMag;
            itemComponent.MuzzelPostion = item.MuzzelPos;
        }
    }

    public int findItem(Item item)
    {
        inventoryItem foundItem = inventoryList.Find(inventoryItem => inventoryItem.itemName == item.name);
        return foundItem.amount;
    }
}

[System.Serializable]
public class inventoryItem
{
    public ItemType itemType;
    public string itemName;
    public int itemID;
    public float itemWeight;
    public bool stackable;

    public bool automatic;
    public float timetoFire;
    public float damage;
    public float spread;
    public int distance;
    public int magSize;
    public Sprite itemObject;

    public float heal;
    public float duration;

    public int amount = 1;
    public int currentMag;
    public bool equipd;

    public Vector2 MuzzelPos;

    public AudioClip audioClip;
    public ParticleSystem bullet;
    public ParticleSystem Muzzelflasch;

    public inventoryItem(inventoryItem inventoryItem, int amount)
    {
        this.itemType = inventoryItem.itemType;
        this.itemName = inventoryItem.itemName;
        this.itemWeight = inventoryItem.itemWeight;
        this.automatic = inventoryItem.automatic;
        this.timetoFire = inventoryItem.timetoFire;
        this.damage = inventoryItem.damage;
        this.spread = inventoryItem.spread;
        this.distance = inventoryItem.distance;
        this.magSize = inventoryItem.magSize;
        this.heal = inventoryItem.heal;
        this.duration = inventoryItem.duration;
        this.itemID = inventoryItem.itemID;
        this.stackable = inventoryItem.stackable;
        this.itemObject = inventoryItem.itemObject;
        this.currentMag = inventoryItem.currentMag;
        this.MuzzelPos = inventoryItem.MuzzelPos;
        this.audioClip = inventoryItem.audioClip;
        this.bullet = inventoryItem.bullet;
        this.Muzzelflasch = inventoryItem.Muzzelflasch;
        this.amount = amount;
    }

    public inventoryItem(ItemType itemType, string itemName, float itemWeight, bool automatic, float timetoFire, float damage, float spread, int distance, int magSize, float heal, float duration, int itemID, bool stackable, Sprite itemObject, int currentMag, Vector2 MuzzelPos, AudioClip audioClip, ParticleSystem bullet, ParticleSystem Muzzelflasch)
    {
        this.itemType = itemType;
        this.itemName = itemName;
        this.itemWeight = itemWeight;
        this.automatic = automatic;
        this.timetoFire = timetoFire;
        this.damage = damage;
        this.spread = spread;
        this.distance = distance;
        this.magSize = magSize;
        this.heal = heal;
        this.duration = duration;
        this.itemID = itemID;
        this.stackable = stackable;
        this.itemObject = itemObject;
        this.currentMag = currentMag;
        this.MuzzelPos = MuzzelPos;
        this.audioClip = audioClip;
        this.bullet = bullet;
        this.Muzzelflasch = Muzzelflasch;
    }
}
