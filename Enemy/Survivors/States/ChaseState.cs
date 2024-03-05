using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public SearchState searchState;
    public AttackState attackState;
    public bool inRange = false;
    public bool canSeeThePlayer;

    public override State RunCurrentState()
    {
        if(canSeeThePlayer){
            return attackState;
        }           
        return this;
    }
    
}
