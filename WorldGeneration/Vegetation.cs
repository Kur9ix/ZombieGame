using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Vegetation : MonoBehaviour
{
    public GameObject prefab;
    public GameObject vegetationHolder;

    public static ObjectPool<GameObject> objectPool;

    public WorldGenerationManager worldGenerationManager;

    private List<GameObject> activeObjects = new List<GameObject>();

    void Awake()
    {

    }

    void Start()
    {
        objectPool = new ObjectPool<GameObject>(createVegetationObject,
        OnTakeFromPool, OnReturnedToPool, null, false, 10, 50
        );
        
    }

    public void checkChunk(int xCord, int yCord)
    {
        if (!checkIfChunkWasGenerated(xCord, yCord))
        {
            generateVegetation(xCord, yCord);
        }
        else
        {
            loadChunkVegetation(xCord, yCord);
        }
    }

    public void generateVegetation(int xCord, int yCord)
    {
        for (int x = xCord * worldGenerationManager.getChunkSize(); x < (xCord+1) * worldGenerationManager.getChunkSize(); x++)
        {
            for (int y =  yCord * worldGenerationManager.getChunkSize(); y <  (yCord +1 ) * worldGenerationManager.getChunkSize(); y++)
            {
                Vector3Int cellPosition = worldGenerationManager.tilemap.WorldToCell(new Vector3((x + y) / 2f, (x - y) / 4f, 0));
                Vector3 worldCellPostion = worldGenerationManager.tilemap.CellToWorld(cellPosition); // spawnpoint but sitll neeed to add radnom offset and amount to spawn in the cell 1-3 or 1-2
                if(worldGenerationManager.tilemap.GetTile(cellPosition) == worldGenerationManager.tiles[0] && Random.Range(0, 1000) <= 10){ // spawnchance
                    var obj = objectPool.Get();
                    obj.transform.position = worldCellPostion;
                    obj.GetComponent<VegetationGameObjectInfo>().chunk = new Vector3Int(xCord, yCord);
                }else if(worldGenerationManager.tilemap.GetTile(cellPosition) == worldGenerationManager.tiles[2] && Random.Range(0, 1000) <= 40) {
                    var obj = objectPool.Get();
                    obj.transform.position = worldCellPostion;
                    obj.GetComponent<VegetationGameObjectInfo>().chunk = new Vector3Int(xCord, yCord);
                }
            }
        }
    }

    public void loadChunkVegetation(int xCord, int yCord)
    {
       
    }

    public void unLoadChunkVegetation(int xCord, int yCord)
    {
        for (int i = 0; i < activeObjects.Count; i++)
        {
            if(activeObjects[i].GetComponent<VegetationGameObjectInfo>().chunk == new Vector3Int(xCord,yCord)){
                objectPool.Release(activeObjects[i]);
            }
        }
    }

    private GameObject createVegetationObject()
    {
        GameObject obj = Instantiate(prefab);
        return obj;
    }

    private void OnReturnedToPool(GameObject obj)
    {
        activeObjects.Remove(obj);
        obj.SetActive(false);
    }

    private void OnTakeFromPool(GameObject obj)
    {
        activeObjects.Add(obj);
        obj.SetActive(true);
    }


    private bool checkIfChunkWasGenerated(int xCord, int yCord)
    {
        foreach(GameObject obj in activeObjects){
            if(obj.GetComponent<VegetationGameObjectInfo>().chunk == new Vector3Int(xCord, yCord)){
                return true;
            }
        }
        return false;
    }

}
