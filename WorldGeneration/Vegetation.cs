using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Pool;

public class Vegetation : MonoBehaviour
{   
    [SerializeField]    
    private int amountToPool;
    [SerializeField]
    private bool usePool;

    [SerializeField] 
    private GameObject VegetationObject;
    
    void Start()
    {
       
    }

    public void generateVegetation(int xCord, int yCord){
       

    }

    public void loadChunkVegetation(int xCord, int yCord){
        // check if chunk has already been generated if not    generateVegetation(xCord, yCord);
        
    }

    public void unLoadChunkVegetation( GameObject obj ){
    
    }

    private void InstantiateObjects(){
        for (int i = 0; i < 100; i++)
        {
            var obj = Instantiate(VegetationObject);
            obj.SetActive(false);
        }
    }

}



// add ResourceStats script to the Vegetation