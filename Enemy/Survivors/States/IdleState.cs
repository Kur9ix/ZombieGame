using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public AttackState attackState;
    public bool canSeeThePlayer;

    public CheckIfPlayerIsInView checkIfPlayerIsInView;

    public override State RunCurrentState()
    {
        if (canSeeThePlayer)
        {
            return attackState;
        }
        else
        {
            // mit audio auf  wir direkt in den such modus gewechselt in der richtung von Sound
        }

        return this;
    }

    void Update()
    {
        canSeeThePlayer = checkIfPlayerIsInView.IfPlayerIsInView();
    }
}
