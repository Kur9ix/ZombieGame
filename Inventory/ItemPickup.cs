using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField]
    private GameObject InventoryManager;

    public LayerMask playerLayer;

    private bool isPickingUp = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isPickingUp)
        {
            StartCoroutine(PickUpDelay());
        }
    }

    IEnumerator PickUpDelay()
    {
        isPickingUp = true;
        pickUpItem();
        yield return new WaitForSeconds(0.1f);
        isPickingUp = false;
    }

    public void pickUpItem()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (direction.magnitude <= 3f)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, playerLayer);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform != null && hit.transform.GetComponent<Item>() != null)
                {
                    Item item = hit.transform.gameObject.GetComponent<Item>();
                    if (InventoryManager.GetComponent<Inventory>().addItem(item) == true)
                    {
                        Destroy(hit.transform.gameObject);
                        break;
                    }
                    else
                    {
                        Debug.LogError("Adding item to inventory failed");
                        break;
                    }
                }
            }
        }
    }
}
