using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{

    [SerializeField]
    private GameObject healtInfo;

    [SerializeField]
    private GameObject staminaInfo;

    [SerializeField]
    private GameObject gunInfo;

    public PlayerStats playerStats;

    public Inventory inventory;

    void Update()
    {
        
    }
}

/*

    healtInfo.GetComponent<Text>().text = "" + playerStats.getHealth();
        staminaInfo.GetComponent<Text>().text = "" + playerStats.getStamina();
        if(inventory.equippedSlot.equipd){
            gunInfo.GetComponent<Text>().text = "" + inventory.equippedSlot.currentMag;
        }

*/