using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
    public Inventory inventory;
    [SerializeField]
    private float weight;

    [SerializeField]
    private List<inventoryItem> storage = new List<inventoryItem>();
    public void addItem(inventoryItem Item, int amount){
        
        if (storage.Find(inventoryItem => inventoryItem.itemName == Item.itemName) == null || Item.stackable == false)
        {
            storage.Add(new inventoryItem(Item, amount));
        }
        else
        {
            storage.Find(inventoryItem => inventoryItem.itemID == Item.itemID).amount += 1;
        }
        
    }

    public void removeItem(inventoryItem Item, int amount){
        for (int i = 0; i < storage.Count; i++)
        {
            if (Item.itemID == storage[i].itemID && storage[i].amount - amount > 0)
            {
                inventory.addItem(Item, amount);
                storage[i].amount -= amount;
                
            }
            else
            {
                inventory.addItem(Item, amount);
                storage.RemoveAt(i);
            }
        }
    }
    
    public List<inventoryItem> getStorageList(){
        return this.storage;
    }
}
