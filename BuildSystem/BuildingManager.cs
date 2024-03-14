using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingManager : MonoBehaviour
{
    public Tilemap tilemap;

    public Tilemap tempMap;
    public List<BuildingInfo> buildings = new List<BuildingInfo>();
    public List<PlacedBuilding> placedBuildings = new List<PlacedBuilding>();

    public void PlacedBuilding(BuildingInfo buildingInfo){
        if(buildingInfo.buildingTyp == buildingTyp.groundTile){
            
        }else
        {
            
        }
    }
}


[System.Serializable]
public class PlacedBuilding
{
   public BuildingInfo buildingInfo;

   public Vector3 buildingPostion;
   public int buildingID;
   public float health;

   public PlacedBuilding(){

   }
}
