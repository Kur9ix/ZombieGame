using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    [SerializeField]

    private float runSpeed;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(Input.GetKey(KeyCode.LeftShift)){
            transform.Translate( new Vector3(horizontal-vertical, (horizontal + vertical) / 2, 0 )* (speed+runSpeed) * Time.deltaTime);
        }else{
            transform.Translate( new Vector3(horizontal-vertical, (horizontal + vertical) / 2, 0 )* speed * Time.deltaTime);
        }

    }
}
