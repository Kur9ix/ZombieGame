using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private GameObject inventorySlotSpace;

    [SerializeField]
    private GameObject equipSpace;

    [SerializeField]
    private GameObject storageUI;

    [SerializeField]
    private GameObject escMenu;

    [SerializeField]
    private GameObject searchabelUi;

    public bool UiActive;
    IEnumerator ToggleUiWithDelay(float delay, bool active, GameObject Ui)
    {
        yield return new WaitForSeconds(delay);
        Ui.SetActive(active);
        UiActive = active;
    }
    IEnumerator ToggleUiWithDelay(float delay, bool active, GameObject Ui, GameObject subUi)
    {
        yield return new WaitForSeconds(delay);
        Ui.SetActive(active);
        subUi.SetActive(active);
        UiActive = active;
    }

    void Update()
    {
        if ((Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.Escape)) && storageUI.activeInHierarchy)
        {
            StartCoroutine(ToggleUiWithDelay(0.3f, false, inventorySlotSpace, storageUI));
        }
        if((Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.Escape)) && searchabelUi.activeInHierarchy){
            StartCoroutine(ToggleUiWithDelay(0.3f, false, inventorySlotSpace, searchabelUi));
        }
        if (Input.GetKey(KeyCode.I) && !UiActive)
        {
            StartCoroutine(ToggleUiWithDelay(0.3f, true, inventorySlotSpace, equipSpace));
            inventorySlotSpace.transform.parent.GetComponent<InventoryUI>().checkForItemChange();
        }
        else if ((Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.Escape)) && inventorySlotSpace.activeInHierarchy)
        {
            StartCoroutine(ToggleUiWithDelay(0.3f, false, inventorySlotSpace, equipSpace));
        }

        if (Input.GetKey(KeyCode.Escape) && !UiActive)
        {
            StartCoroutine(ToggleUiWithDelay(0.3f, true, escMenu));
        }
        else if (Input.GetKey(KeyCode.Escape) && escMenu.activeInHierarchy)
        {
            StartCoroutine(ToggleUiWithDelay(0.3f, false, escMenu));
        }
    }

    public void openStorageUI(GameObject storageObj)
    {
        StartCoroutine(ToggleUiWithDelay(0.3f, true, inventorySlotSpace, storageUI));
        inventorySlotSpace.transform.parent.GetComponent<InventoryUI>().checkForItemChange();
        storageUI.GetComponent<StorageUi>().UpdateStorageUi(storageObj);
    }

    public void openSearchableUi(GameObject searchableObj){
        StartCoroutine(ToggleUiWithDelay(0.3f, true, inventorySlotSpace, searchabelUi));
        inventorySlotSpace.transform.parent.GetComponent<InventoryUI>().checkForItemChange();
        searchabelUi.GetComponent<SearchableUi>().UpdateSearchabelUI(searchableObj);
    }

}
