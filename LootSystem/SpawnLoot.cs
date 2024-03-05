using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLoot : MonoBehaviour
{

    public List<GameObject> enemyItems = new List<GameObject>();
    public List<GameObject> zombieItems = new List<GameObject>();

    public void spawnLoot(Vector3 pos, GameObject Item)
    {
        Vector3 spawnTransform = new Vector3(pos.x + Random.Range(-10, 10) / 100, pos.y + Random.Range(-10, 10) / 100, 0);
        Quaternion spawnRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        Instantiate(Item, spawnTransform, spawnRotation);
    }

    public void spawnEnemyLoot(Vector3 pos)
    {
        for (int j = 0; j < Random.Range(2, 4); j++)
        {
            Vector3 spawnPosition = new Vector3(pos.x + Random.Range(-10, 10) / 100, pos.y + Random.Range(-10, 10) / 100, 0);
            Quaternion spawnRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            Instantiate(enemyItems[Random.Range(0, enemyItems.Count)], spawnPosition, spawnRotation);
        }


    }

    public void spawnZombieLoot(Vector3 pos)
    {
        for (int j = 0; j < Random.Range(2, 4); j++)
        {
            Vector3 spawnTransform = new Vector3(pos.x + Random.Range(-10, 10) / 100, pos.y + Random.Range(-10, 10) / 100, 0);
            Quaternion spawnRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            Instantiate(zombieItems[Random.Range(0, zombieItems.Count)], spawnTransform, spawnRotation);
        }

    }
}
