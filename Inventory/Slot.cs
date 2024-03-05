using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour, IDropHandler
{
    public int SlotId;
    public bool slotUsed;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Droped");
        if (eventData.pointerDrag != null && slotUsed == false)
        {
            eventData.pointerDrag.GetComponent<DragAndDrop>().lastSlot.GetComponent<Slot>().slotUsed = false;

            if (eventData.pointerDrag.GetComponent<DragAndDrop>().equipedItem)
            {
                eventData.pointerDrag.GetComponent<DragAndDrop>().equipedItem = false;
                GameObject.Find("InventoryManager").GetComponent<Inventory>().unEquipItem();

            }
            if(eventData.pointerDrag.GetComponent<DragAndDrop>().lastSlot.tag == "storageSlot" && tag == "slot"){
                GameObject.Find("StorageUi").GetComponent<StorageUi>().switchItemToInventory(eventData.pointerDrag.GetComponent<DragAndDrop>().item);
                eventData.pointerDrag.transform.SetParent(GameObject.Find("InventorySlotSpace").transform);
            }else if(eventData.pointerDrag.GetComponent<DragAndDrop>().lastSlot.tag == "slot" && tag == "storageSlot"){
                GameObject.Find("StorageUi").GetComponent<StorageUi>().switchItemToStorage(eventData.pointerDrag.GetComponent<DragAndDrop>().item);
                eventData.pointerDrag.transform.SetParent(GameObject.Find("StorageSlotSpace").transform);
            }
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.GetComponent<DragAndDrop>().lastSlot = gameObject;
            slotUsed = true;
        }
        else if (eventData.pointerDrag != null && slotUsed)
        {
            print(eventData.pointerDrag.GetComponent<DragAndDrop>().lastSlot.transform.name);

        }

    }
}
