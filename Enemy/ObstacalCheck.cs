using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacalCheck : MonoBehaviour
{
    bool obstacle; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        obstacle = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        obstacle = false;
    }

    public bool checkForObstacal(){
        return obstacle;
    }
}
