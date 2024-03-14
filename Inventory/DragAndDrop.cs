using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public inventoryItem item;
    public GameObject lastSlot;

    public bool equipedItem = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("DragBegin");
        canvasGroup.alpha = 0.5f;
        gameObject.transform.SetParent(GameObject.Find("Canvas").transform);
        DeactivateRaycastTargets();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Draging");

        rectTransform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("DragEnd");
        canvasGroup.alpha = 1f;
        ReactivateRaycastTargets();

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            print(result);
            if (result.gameObject.CompareTag("dropArea"))
            {
                GameObject.Find("InventoryUi").GetComponent<InventoryUI>().removeItemFormUI(item.itemID);
                GameObject.Find("InventoryManager").GetComponent<Inventory>().dropItem(item, 1);
                Destroy(gameObject);
            }
            if (result.gameObject.GetComponent<Slot>() && !result.gameObject.GetComponent<Slot>().slotUsed)
            {
                if (equipedItem && lastSlot != result.gameObject)
                {
                    equipedItem = false;
                    GameObject.Find("InventoryManager").GetComponent<Inventory>().unEquipItem();
                }

                if (result.gameObject.CompareTag("handEquipSlot") && !result.gameObject.GetComponent<Slot>().slotUsed)
                {
                    GameObject.Find("InventoryManager").GetComponent<Inventory>().equipItem(item, item.itemID);
                    equipedItem = true;
                }

                if (result.gameObject.CompareTag("slot") && lastSlot.CompareTag("storageSlot"))
                {
                    lastSlot.GetComponent<Slot>().slotUsed = false;
                    GameObject.Find("StorageUi").GetComponent<StorageUi>().switchItemToInventory(item, gameObject);
                    lastSlot = result.gameObject;
                    result.gameObject.GetComponent<Slot>().slotUsed = true;
                }

                if (result.gameObject.CompareTag("storageSlot") && lastSlot.CompareTag("slot"))
                {

                    lastSlot.GetComponent<Slot>().slotUsed = false;
                    GameObject.Find("StorageUi").GetComponent<StorageUi>().switchItemToStorage(item, gameObject);
                    lastSlot = result.gameObject;
                    result.gameObject.GetComponent<Slot>().slotUsed = true;

                }

                if (result.gameObject.CompareTag("slot") && lastSlot.CompareTag("searchabelSlot"))
                {
                    GameObject.Find("InventoryManager").GetComponent<Inventory>().addItem(item, item.amount);
                    GameObject.Find("SearchabelSlotSpace").GetComponent<SearchableUi>().removeItemsFromStorage(item);
                    lastSlot = result.gameObject;
                    GameObject.Find("InventoryUi").GetComponent<InventoryUI>().checkForItemChange();
                    Destroy(gameObject);
                }
                if (lastSlot != result.gameObject)
                {
                    lastSlot.gameObject.GetComponent<Slot>().slotUsed = false;
                    lastSlot = result.gameObject;
                    result.gameObject.GetComponent<Slot>().slotUsed = true;
                }
            }
        }

        gameObject.transform.SetParent(lastSlot.transform.parent);
        rectTransform.anchoredPosition = lastSlot.GetComponent<RectTransform>().anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("PointerDown");
    }


    void DeactivateRaycastTargets()
    {
        foreach (GameObject obj in GameObject.Find("InventoryUi").GetComponent<InventoryUI>().ItemObjects)
        {
            obj.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    void ReactivateRaycastTargets()
    {
        foreach (GameObject obj in GameObject.Find("InventoryUi").GetComponent<InventoryUI>().ItemObjects)
        {
            obj.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }

    public void switchItem()
    {

    }


}
