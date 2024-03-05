using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieChaseState : State
{
    [SerializeField]
    private GameObject soundManager;
    private Vector3 soundPostion;
    public bool nearThePlayer;
    public override State RunCurrentState()
    {
        throw new System.NotImplementedException(); // move to 
    }

    void Update(){
        //soundPostion = soundManager.GetComponent<>().soundPostion;
    }
}
