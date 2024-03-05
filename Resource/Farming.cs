using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farming : MonoBehaviour
{
    [SerializeField]
    private Sprite treeStump;
    public bool equipdTool;
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private List<GameObject> resources = new List<GameObject>();
    private float timeToFarm;

    
    void Update(){
        if(equipdTool){
            if(Input.GetKey(KeyCode.Mouse0) && Time.time > timeToFarm)
            {
                StartCoroutine(Farmdelay());
            }
        }
    }

    IEnumerator Farmdelay(){
        StartFarming();
        timeToFarm = Time.time + inventory.equippedSlot.duration;
        yield return new WaitForSeconds(inventory.equippedSlot.duration);
    }

    private void StartFarming(){
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePosition);
        //if(hit && hit.transform.position -transform.position >= 2 && hit.transform.GetComponent<ResourceStats>().outcome > 0){  }   
        if(hit.transform.GetComponent<ResourceStats>().Typ == ResourceStats.resourceTyp.tree){
            GameObject.Find("LootSpawnManager").GetComponent<SpawnLoot>().spawnLoot(hit.transform.position, resources[0]);
            hit.transform.GetComponent<ResourceStats>().outcome -= 1;
            
        } else if(hit.transform.GetComponent<ResourceStats>().Typ == ResourceStats.resourceTyp.stone){
            GameObject.Find("LootSpawnManager").GetComponent<SpawnLoot>().spawnLoot(hit.transform.position, resources[1]);
            hit.transform.GetComponent<ResourceStats>().outcome -= 1;
        }else if(hit.transform.GetComponent<ResourceStats>().Typ == ResourceStats.resourceTyp.fiber){
            GameObject.Find("LootSpawnManager").GetComponent<SpawnLoot>().spawnLoot(hit.transform.position, resources[2]);
            hit.transform.GetComponent<ResourceStats>().outcome -= 1;
        }

        if(hit.transform.GetComponent<ResourceStats>().outcome == 0 && hit.transform.GetComponent<ResourceStats>().Typ == ResourceStats.resourceTyp.tree){
            if(hit.transform.GetComponent<SpriteRenderer>() != null){
                hit.transform.GetComponent<SpriteRenderer>().sprite = treeStump;  //change tree sprtie to tree stump 
            }else{
                Debug.LogError("SpriteRenderer not found");
            }
        }else{
            Destroy(hit.transform.gameObject); // remove the Resource
        }
    }


}
