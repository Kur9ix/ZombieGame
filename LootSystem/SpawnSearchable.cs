using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSearchable : MonoBehaviour
{
    public enum searchableType
    {
        test,
        kitchen,
        living,
        garage,
        trash,
        car,

    }
    List<inventoryItem> lootItems = new List<inventoryItem>();
    public List<inventoryItem> lootItemsKitchen = new List<inventoryItem>();
    public List<inventoryItem> lootItemsGarage = new List<inventoryItem>();
    public GameObject prefab;
    public void SpawnSearchableObject(Vector3 pos, searchableType type)
    {
        if (type == searchableType.kitchen)
        {
            GameObject searchable = Instantiate(prefab);
            prefab.AddComponent<SearchableStorage>();
            prefab.transform.position = pos;
            SearchableStorage storage = prefab.GetComponent<SearchableStorage>();
            int amount = Random.Range(1, 6);
            for (int i = 0; i < amount; i++)
            {
                storage.addToStorage(lootItemsKitchen[Random.Range(1, lootItems.Count)]);
            }
        }
        if (type == searchableType.garage)
        {
            GameObject searchable = Instantiate(prefab);
            prefab.AddComponent<SearchableStorage>();
            SearchableStorage storage = prefab.GetComponent<SearchableStorage>();
            int amount = Random.Range(1, 6);
            for (int i = 0; i < amount; i++)
            {
                storage.addToStorage(lootItemsGarage[Random.Range(1, lootItems.Count)]);
            }
        }
    }

    void Start(){
        SpawnSearchableObject(new Vector3(10,5,0), searchableType.kitchen);
    }
}
