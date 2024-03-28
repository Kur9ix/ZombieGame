using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageUi : MonoBehaviour
{
    [SerializeField]
    private GameObject itemPrefab;

    [SerializeField]
    private List<GameObject> slots = new List<GameObject>();
    public List<GameObject> ItemObjects = new List<GameObject>();

    public InventoryUI inventoryUI;

    public GameObject slotspace;
    public GameObject storageObject;

    public void UpdateStorageUi(GameObject storageObj)
    {
        print("Updated Storage UI");
        storageObject = storageObj;
        if (ItemObjects.Count != storageObj.GetComponent<StorageManager>().getStorageList().Count)
        {
            print("list difference");
            getListDifference(storageObj.GetComponent<StorageManager>().getStorageList());
        }
        UpdateItemInfo(storageObj.GetComponent<StorageManager>().getStorageList());
    }


    void getListDifference(List<inventoryItem> items)
    {
        print("List Difference found");
        if (ItemObjects.Count < items.Count)
        {
            print(items.Count - ItemObjects.Count);
            addItemsToStorage(items.Count - ItemObjects.Count, items);
        }
        else
        {
            removeItemsFromStorage(ItemObjects.Count - items.Count);
        }
    }

    void addItemsToStorage(int amount, List<inventoryItem> items)
    {
        for (int i = 0; i < amount; i++)
        {
            print(i);
            if (!slots[i].GetComponent<Slot>().slotUsed)
            {
                var obj = Instantiate(itemPrefab);
                obj.transform.SetParent(slotspace.transform);
                if (obj.GetComponent<DragAndDrop>() == null)
                {
                    obj.AddComponent<DragAndDrop>();
                }
                obj.GetComponent<DragAndDrop>().item = items[i];
                obj.GetComponent<DragAndDrop>().lastSlot = slots[i];
                slots[i].GetComponent<Slot>().slotUsed = true;
                obj.GetComponent<RawImage>().texture = items[i].itemObject.texture;
                ItemObjects.Add(obj);
                obj.GetComponent<RectTransform>().anchoredPosition = slots[i].GetComponent<RectTransform>().anchoredPosition;
                break;
            }
        }
    }

    void removeItemsFromStorage(int amount)
    {
        print("removeimng UI elements to Storage");
        for (int i = ItemObjects.Count - amount; i < ItemObjects.Count; i++)
        {
            ItemObjects[i].GetComponent<DragAndDrop>().lastSlot.GetComponent<Slot>().slotUsed = false;
            Destroy(ItemObjects[i]);
            ItemObjects.RemoveAt(i);
        }
    }

    public void removeItemsFromStorage(inventoryItem item, GameObject itemObj)
    {
        List<inventoryItem> itemList = storageObject.GetComponent<StorageManager>().getStorageList();

        itemList.RemoveAt(itemList.FindIndex(inventoryItem => inventoryItem.itemID == item.itemID));
        ItemObjects.RemoveAt(ItemObjects.FindIndex(GameObject => GameObject.GetComponent<DragAndDrop>().item.itemID == item.itemID));

        GameObject.Find("InventoryUi").GetComponent<InventoryUI>().addItem(itemObj);
    }

    void addItemsToStorage(inventoryItem item, GameObject itemObj)
    {
        ItemObjects.Add(itemObj);
        storageObject.GetComponent<StorageManager>().addItem(item, item.amount);
        GameObject.Find("InventoryUi").GetComponent<InventoryUI>().removeItem(itemObj);
    }

    void UpdateItemInfo(List<inventoryItem> items)
    {
        print("updating UI elements to Storage");
        for (int i = 0; i < items.Count; i++)
        {
            ItemObjects[i].GetComponent<DragAndDrop>().item = items[i];
        }

    }

    public void switchItemToStorage(inventoryItem item, GameObject itemObj)
    {
        GameObject.Find("InventoryManager").GetComponent<Inventory>().removeItem(item, item.amount);
        addItemsToStorage(item, itemObj);

    }
    public void switchItemToInventory(inventoryItem item, GameObject itemObj)
    {
        GameObject.Find("InventoryManager").GetComponent<Inventory>().addItem(item, item.amount);
        removeItemsFromStorage(item, itemObj);
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
