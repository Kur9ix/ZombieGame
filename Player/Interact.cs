using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField]
    private GameObject interactUI;


    void Start(){
        interactUI.SetActive(false);
    }
    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (direction.magnitude <= 3f)
        {
            RaycastHit2D hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit2D && hit2D.transform.tag == "interact")
            {
                interactUI.transform.position = new Vector3(hit2D.transform.position.x+0.2f, hit2D.transform.position.y+0.2f, -1);
                interactUI.SetActive(true);
                if(Input.GetKeyDown(KeyCode.E) && hit2D.transform.GetComponent<StorageManager>() != null){
                    GameObject.Find("UiManager").GetComponent<UiManager>().openStorageUI(hit2D.transform.gameObject);
                } 
                if(Input.GetKeyDown(KeyCode.E) && hit2D.transform.GetComponent<SearchableStorage>() != null){
                    GameObject.Find("UiManager").GetComponent<UiManager>().openSearchableUi(hit2D.transform.gameObject);
                }
            }else{
                interactUI.SetActive(false);
            }
        }
    }
}
