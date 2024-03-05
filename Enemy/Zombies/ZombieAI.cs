using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public State currentZombieState;
    void Update(){
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        State nextState = currentZombieState?.RunCurrentState();

        if(nextState != null)
        {
            SwitchState(nextState);
        }   
    }

    private void SwitchState(State nextState){
        currentZombieState = nextState;
    }
}
