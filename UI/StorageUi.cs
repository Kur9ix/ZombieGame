using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageUi : MonoBehaviour
{
    [SerializeField]
    private GameObject itemPrefab;

    [SerializeField]
    private List<GameObject> slots = new List<GameObject>();
    public List<GameObject> ItemObjects = new List<GameObject>();

    public InventoryUI inventoryUI;


    public GameObject storageObject;

    public void UpdateStorageUi(GameObject storageObj)
    {
        print("Updated Storage UI");
        storageObject = storageObj;
        if (ItemObjects.Count != storageObj.GetComponent<StorageManager>().getStorageList().Count)
        {
            getListDifference(storageObj.GetComponent<StorageManager>().getStorageList());
        }
        UpdateItemInfo(storageObj.GetComponent<StorageManager>().getStorageList());
    }


    void getListDifference(List<inventoryItem> items)
    {
        print("List Difference found");
        if (ItemObjects.Count < items.Count)
        {
            addItemsToStorage(items.Count - ItemObjects.Count);
        }
        else
        {
            removeItemsFromStorage(ItemObjects.Count - items.Count);
        }
    }

    void addItemsToStorage(int amount)
    {
        print("adding UI elements to Storage");
        GameObject item = Instantiate(itemPrefab);
        for (int i = 0; i < slots.Count; i++)
        {
            if (!slots[i].GetComponent<Slot>().slotUsed)
            {
                item.GetComponent<RectTransform>().anchoredPosition = slots[i].GetComponent<RectTransform>().anchoredPosition;
                item.GetComponent<DragAndDrop>().lastSlot = slots[i];
            }
        }
        ItemObjects.Add(item);
    }

    void removeItemsFromStorage(int amount)
    {
        print("removeimng UI elements to Storage");
        for (int i = ItemObjects.Count - amount; i < ItemObjects.Count; i++)
        {
            ItemObjects.RemoveAt(i);
        }
    }

    void UpdateItemInfo(List<inventoryItem> items)
    {
        print("updating UI elements to Storage");
        for (int i = 0; i < items.Count; i++)
        {
            ItemObjects[i].GetComponent<DragAndDrop>().item = items[i];
        }

    }

    public void switchItemToStorage(inventoryItem item)
    {
        print("switchS");
        GameObject.Find("InventoryManager").GetComponent<Inventory>().removeItem(item.itemID);
        storageObject.GetComponent<StorageManager>().addItem(item, item.amount);
        GameObject itemObj = Instantiate(itemPrefab);
        itemObj.GetComponent<DragAndDrop>().item = item;
        this.ItemObjects.Add(itemObj);
        inventoryUI.ItemObjects.Find(GameObject => GameObject.GetComponent<DragAndDrop>().item == item).GetComponent<CanvasGroup>().blocksRaycasts = true;
        inventoryUI.ItemObjects.RemoveAt(inventoryUI.ItemObjects.FindIndex(GameObject => GameObject.GetComponent<DragAndDrop>().item == item));
    }
    public void switchItemToInventory(inventoryItem item)
    {
        print("switchI");
        GameObject.Find("InventoryManager").GetComponent<Inventory>().addItem(item, item.amount);
        storageObject.GetComponent<StorageManager>().removeItem(item, item.amount);
        GameObject itemObj = Instantiate(itemPrefab);
        itemObj.GetComponent<DragAndDrop>().item = item;
        inventoryUI.ItemObjects.Add(itemObj);
        inventoryUI.ItemObjects.Find(GameObject => GameObject.GetComponent<DragAndDrop>().item == item).GetComponent<CanvasGroup>().blocksRaycasts = true;
        ItemObjects.RemoveAt(ItemObjects.FindIndex(GameObject => GameObject.GetComponent<DragAndDrop>().item == item));
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
