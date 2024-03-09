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

    IEnumerator MoveAlongPath(List<Vector3Int> path, Vector3Int targetpos)
    {
        foreach (Vector3Int vector in path)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, vector, moveSpeed * Time.deltaTime);
            yield return null;

            if(transform.position == targetpos){
                break;
            }
    
            print("Moved to: " + vector);
        }
        pathCalculated = false;
    }

    void CalculatePath()
    {
        List<Vector3Int> path = pathfinding.pathfindingPoints(transform.position, checkIfPlayerIsInView.lastPlayerPos);
        StartCoroutine(MoveAlongPath(path, pathfinding.targetPoint.position));
    }

}
