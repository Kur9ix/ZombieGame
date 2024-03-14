using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchableUi : MonoBehaviour
{
    public GameObject itemPrefab;

    [SerializeField]
    private List<GameObject> slots = new List<GameObject>();
    [SerializeField]
    private List<GameObject> searchableItems = new List<GameObject>();

    GameObject searchableObject;
    public void UpdateSearchabelUI(GameObject searchableObj)
    {
        if (searchableItems.Count != searchableObj.GetComponent<SearchableStorage>().storage.Count)
        {
            if (searchableItems.Count > searchableObj.GetComponent<SearchableStorage>().storage.Count)
            {
                removeItemsFromStorage(searchableItems.Count - searchableObj.GetComponent<SearchableStorage>().storage.Count);
            }
            else
            {
                addItemsToStorage(searchableObj.GetComponent<SearchableStorage>().storage.Count);
            }
        }
        searchableObject = searchableObj;
        updateItems();
    }

    public void updateItems(){
        for (int j = 0; j < searchableItems.Count; j++)
        {
            if (!slots[j].GetComponent<Slot>().slotUsed)
            {
                searchableItems[j].GetComponent<DragAndDrop>().item = searchableObject.GetComponent<SearchableStorage>().storage[j];
                searchableItems[j].GetComponent<DragAndDrop>().lastSlot = slots[j];
                slots[j].GetComponent<Slot>().slotUsed = true;
                searchableItems[j].transform.SetParent(gameObject.transform);
                searchableItems[j].GetComponent<RawImage>().texture = searchableObject.GetComponent<SearchableStorage>().storage[j].itemObject.texture;
                searchableItems[j].GetComponent<RectTransform>().anchoredPosition = slots[j].GetComponent<RectTransform>().anchoredPosition;
            }
        }
    }

    void addItemsToStorage(int amount)
    {
        print("Add");
        for (int i = searchableItems.Count; i < amount; i++)
        {
            GameObject itemObj = Instantiate(itemPrefab);
            searchableItems.Add(itemObj);
        }

    }

    public void removeItemsFromStorage(int amount)
    {
        print("remove");
        for (int i = 0; i < amount; i++)
        {
            searchableItems.RemoveAt(i);
        }
    }

    public void removeItemsFromStorage(inventoryItem item)
    {
        for (int i = 0; i < searchableItems.Count; i++)
        {
            if (item.itemID == searchableItems[i].GetComponent<DragAndDrop>().item.itemID)
            {
                print("removed Item");
                searchableItems.RemoveAt(i);
            }
        }
        updateItems();
    }



}
