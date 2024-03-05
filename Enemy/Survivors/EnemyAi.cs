using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyAi : MonoBehaviour
{
    public State currentState;
    void Update(){
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        State nextState =currentState?.RunCurrentState();

        if(nextState != null)
        {
            SwitchState(nextState);
        }   
    }

    private void SwitchState(State nextState){
        currentState = nextState;
    }
}

