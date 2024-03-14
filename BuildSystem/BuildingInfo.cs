using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum buildingTyp{
    groundTile,
    freeToPlace
}

public enum buildingResources{
    wood
}
[System.Serializable]
public class BuildingInfo{
   public String buildingName;
   public buildingTyp buildingTyp;
   public List<buildingResources> buildingResources = new List<buildingResources>();
   public float maxHealth;
   public TileBase groundTileBase;
   public Sprite palceableObjects;
   public AudioClip buildSound;
}
