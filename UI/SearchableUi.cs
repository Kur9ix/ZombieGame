using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchableUi : MonoBehaviour
{
    private List<GameObject> slots = new List<GameObject>();
    private List<GameObject> searchableItems = new List<GameObject>();
    public void UpdateSearchabelUI(GameObject searchableObj){
        if(searchableItems.Count != searchableObj.GetComponent<SearchableStorage>().storage.Count){

        }
    }
}
