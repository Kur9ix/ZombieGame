using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchableStorage : MonoBehaviour
{
    public List<inventoryItem> storage = new List<inventoryItem>();

    public void addToStorage(inventoryItem item){
        storage.Add(item);
    }
    
    public void removeItem(inventoryItem item){
        storage.RemoveAt(storage.FindIndex(inventoryItem => inventoryItem.itemID == item.itemID));
    }
}
