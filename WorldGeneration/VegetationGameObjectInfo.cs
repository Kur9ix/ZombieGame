using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetationGameObjectInfo : MonoBehaviour
{
    public Vector3Int chunk;
    public Sprite sprite;
    public Vector3 objectPos;


    void Awake(){
       // GetComponent<SpriteRenderer>().sprite = sprite;
    }
}


