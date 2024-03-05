using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : State
{
    public CheckIfPlayerIsInView checkIfPlayerIsInView;
    public AttackState attackState;
    public IdleState idleState;

    public Pathfinding pathfinding;
    public bool canSeeThePlayer;

    public float timer = 30f;
    public float moveSpeed = 4f;
    bool pathCalculated = false;
    public override State RunCurrentState()
    {
        if (canSeeThePlayer)
        {
            return attackState;
        }
        else
        {
            if (!pathCalculated) // Nur wenn der Pfad noch nicht berechnet wurde
            {
                CalculatePath(); // Pfad berechnen
                pathCalculated = true; // Pfad als berechnet markieren
            }
            return searchPlayer(); // Nach dem Spieler suchen
        }
    }

    State searchPlayer()
    {
        timer -= Time.deltaTime;


        if (timer <= 0f)
        {
            timer = 30f;            // sichtraduis erweiter sich 
            return idleState;
        }
        return this;
    }

    void Update()
    {
        canSeeThePlayer = checkIfPlayerIsInView.IfPlayerIsInView();
    }

    void CalculatePath()
    {
        List<Vector3Int> path = pathfinding.pathfindingPoints(transform.position, checkIfPlayerIsInView.lastPlayerPos);

        foreach (Vector3Int vector in path)
        {
            Vector3 direction = (vector - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            print("moving");
            
        }
        pathCalculated = false;
    }



}
