using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStats : MonoBehaviour
{
    public enum resourceTyp{
        tree,
        stone,
        fiber, // for ropes or something like that 
    }
    public int outcome; // how often you can get something from the resource before its destroyd 
    public resourceTyp Typ; 
    
}
