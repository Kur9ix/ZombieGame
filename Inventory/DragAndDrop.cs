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
        gameObject.transform.SetParent(GameObject.Find("InventoryUi").transform);
        DeactivateRaycastTargets();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Draging");

        rectTransform.anchoredPosition += eventData.delta;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("DragEnd");
        canvasGroup.alpha = 1f;
        ReactivateRaycastTargets();

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        print("hit34erweawedawet");

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("dropArea"))
            {
                GameObject.Find("InventoryUi").GetComponent<InventoryUI>().removeItemFormUI(item.itemID);
                GameObject.Find("InventoryManager").GetComponent<Inventory>().dropItem(item, 1);
                Destroy(gameObject);
            }
            if (result.gameObject.CompareTag("handEquipSlot") && !equipedItem)
            {
                GameObject.Find("InventoryManager").GetComponent<Inventory>().equipItem(item, item.itemID);
                lastSlot = GameObject.Find("HandEquipSlot");
                equipedItem = true;
                break;
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

    public void switchItem(){

    }


}
