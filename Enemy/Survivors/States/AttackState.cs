using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public CheckIfPlayerIsInView checkIfPlayerIsInView;
    public SearchState searchState;
    public Transform player;
    public LayerMask enemyLayer;
    public float shootDelayTime = 1f;

    private bool shooting;
    public GameObject playerObject;
    float gunRange = 10f;
    public float accuracy = 0.8f;
    public bool inCover;
    public bool canSeeThePlayer;

    float timer = 2.5f;
    public override State RunCurrentState()
    {
        Debug.Log("Atacking");
        if(!shooting){
            StartCoroutine(shootDelay());
            shooting = true;
        }
        if (!canSeeThePlayer)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer = 3f;
                return searchState;
            }
        }
        return this;
    }

    IEnumerator shootDelay()
    {
        yield return new WaitForSeconds(shootDelayTime);
        shootPlayer();
        shooting = false;
    }

    public void shootPlayer()
    {
        print("shooting Player");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, 15, 3);
        if (hit && hit.transform.tag == "player" && playerObject.GetComponent<PlayerStats>().getHealth() > 0)
        {
            print(hit.transform.tag);                                             // offset einbringen 
            playerObject.GetComponent<PlayerStats>().setHealth(10);
        }
        else
        {
            print("no hit");
        }
    }

    void Update()
    {
        canSeeThePlayer = checkIfPlayerIsInView.IfPlayerIsInView();
    }

}
