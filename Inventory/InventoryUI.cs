using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public GameObject itemPrefab;

    public GameObject itemSpace;

    private InventoryUiItem[] items = new InventoryUiItem[20];
    [SerializeField]
    private List<GameObject> slots = new List<GameObject>();

    public List<GameObject> ItemObjects = new List<GameObject>();

    public void checkForItemChange()
    {
        foreach (inventoryItem item in inventory.inventoryList)
        {
            UpdateItems(item);
        }
    }


    void UpdateItems(inventoryItem item)
    {
        bool itemAlreadyExists = false;
        foreach (InventoryUiItem uiItem in items)
        {
            if (uiItem != null && uiItem.id == item.itemID)
            {
                itemAlreadyExists = true;
            }
        }
        if (!itemAlreadyExists)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (!slots[i].GetComponent<Slot>().slotUsed)
                {
                    items[i] = new InventoryUiItem(item, item.itemID);
                    var obj = Instantiate(itemPrefab);
                    obj.transform.SetParent(itemSpace.transform);
                    obj.GetComponent<RectTransform>().anchoredPosition = slots[i].GetComponent<RectTransform>().anchoredPosition;
                    if(obj.GetComponent<DragAndDrop>() == null){
                        obj.AddComponent<DragAndDrop>();
                    }
                    obj.GetComponent<DragAndDrop>().item = item;
                    obj.GetComponent<DragAndDrop>().lastSlot = slots[i];
                    slots[i].GetComponent<Slot>().slotUsed = true;
                    obj.GetComponent<RawImage>().texture = item.itemObject.texture;
                    ItemObjects.Add(obj);
                    break;
                }
            }
        }
    }

    public void removeItemFormUI(int id)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].id == id)
            {
                items[i] = null;
                ItemObjects.Remove(ItemObjects.Find(GameObject => GameObject.GetComponent<DragAndDrop>().item.itemID == id));
                break;
            }
        }
    }


    public class InventoryUiItem
    {
        public inventoryItem item;
        public int id;
        public InventoryUiItem(inventoryItem item, int id)
        {
            this.item = item;
            this.id = id;
        }
    }
}
