using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public DeathState deathState;
    [SerializeField] 
    private float health;
    public void updateHealth(float health){
        this.health -= health;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0 ){
            death();
        }
    }

    private void death(){
        this.gameObject.GetComponent<EnemyAi>().currentState = deathState;
    }
}
