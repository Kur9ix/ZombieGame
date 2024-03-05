using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float health;

    [SerializeField]
    private float stamina;


    public float getHealth(){
        return health;
    }

    public void setHealth(float health){
        this.health -= health;
    }

    public float getStamina(){
        return stamina;
    }

    public void setStamina(float stamina){
        this.stamina = stamina;
    }

    public void Update(){
        if(health <= 0){
            playerDeath();
        }
    }

    public void playerDeath(){
        /* Player Death Funktion
        if(difficulty == "easy"){
            //show death menu
            GameObject.Find("InventoryManager").GetComponent<Inventory>().inventoryList.Clear();
            GameObject.Find("InventoryManager").GetComponent<Inventory>().equippedSlot = null;
        }

        */
        print("player Dead"); 

    }
}
