using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class DeathState : State
{
    public GameObject parent;
    public GameObject sParent;

    bool lootDroped = false;
    public SpawnLoot spawnLoot;
    public override State RunCurrentState()
    {   
        death();
        return this;
    }

    void death(){
        GameObject lootSpawnManager = GameObject.Find("LootSpawnManager");
        if (lootSpawnManager != null && !lootDroped) {
            spawnLoot = lootSpawnManager.GetComponent<SpawnLoot>();
            if (spawnLoot != null) {
                parent = this.transform.parent.gameObject;
                sParent = parent.transform.parent.gameObject;
                Rigidbody2D rigidbody2D = sParent.GetComponent<Rigidbody2D>();

                rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
                sParent.GetComponent<CheckIfPlayerIsInView>().enemydeath = true;
                //change sprite to deathsprite and freet player pos 
                spawnLoot.spawnEnemyLoot(sParent.transform.position);
                lootDroped = true;
            } else {
                Debug.LogError("SpawnLoot component not found on LootSpawnManager.");
            }
        }
}


}
