using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchableUi : MonoBehaviour
{
    public GameObject itemPrefab;

    [SerializeField]
    private List<GameObject> slots = new List<GameObject>();
    private List<GameObject> searchableItems = new List<GameObject>();
    public void UpdateSearchabelUI(GameObject searchableObj){
        if(searchableItems.Count != searchableObj.GetComponent<SearchableStorage>().storage.Count){
            if(searchableItems.Count > searchableObj.GetComponent<SearchableStorage>().storage.Count){

            }else {
                
            }
        }
        for (int i = 0; i < searchableItems.Count; i++)
        {
            searchableItems[i].GetComponent<DragAndDrop>().item = searchableObj.GetComponent<SearchableStorage>().storage[i];
            searchableItems[i].GetComponent<RectTransform>().anchoredPosition = slots[i].GetComponent<RectTransform>().anchoredPosition;
        }
    }

    void addItemsToStorage()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            
        }
        
    }

    void removeItemsFromStorage(int amount)
    {
       
    }

}
