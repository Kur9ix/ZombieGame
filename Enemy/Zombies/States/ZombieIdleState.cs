using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ZombieIdleState : State
{
    public ZombieChaseState zombieChaseState;
    public bool canHearThePlayer;
    public override State RunCurrentState()
    {
        if(canHearThePlayer){
            return zombieChaseState;
        }

        return this;
    }
}
