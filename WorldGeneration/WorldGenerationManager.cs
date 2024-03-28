using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerationManager : MonoBehaviour
{
    public Tilemap tilemap;
    public List<TileBase> tiles = new List<TileBase>();
    public Transform playerTransform;

    [SerializeField]
    private PerlinNoise PerlinNoise;
    [SerializeField]
    private GameObject RoadGenerator;
    [SerializeField]
    private Vegetation VegetationGenerator;
    [SerializeField]
    private int chunkSize;

    public Vector3Int playerCellPos;
    private Vector3Int lastPlayerCellPos;
    public int chunkX;
    public int chunkY;

    private Dictionary<Vector2Int, bool> loadedChunks = new Dictionary<Vector2Int, bool>();
    void FixedUpdate()
    {
        playerCellPos = tilemap.WorldToCell(playerTransform.position);
        chunkX = Mathf.FloorToInt((float)playerCellPos.x / chunkSize);
        chunkY = Mathf.FloorToInt((float)playerCellPos.y / chunkSize);

        if (playerCellPos != lastPlayerCellPos) // check if player is moving
        {
            loadChunks(chunkX, -chunkY - 1);
            CheckIfChunkNeedsToBeLoaded(chunkX, -chunkY - 1);
            lastPlayerCellPos = playerCellPos;
        }
    }


    void loadChunks(int xCord, int yCord)
    {
        for (int x = xCord - 1; x <= xCord + 1; x++)
        {
            for (int y = yCord - 1; y <= yCord + 1; y++)
            {
                GenerateChunk(x, y);
                VegetationGenerator.checkChunk(xCord,yCord);
            }
        }
    }

    void unLoadChunk(int xCord, int yCord)
    {
        for (int x = xCord * chunkSize; x < (xCord + 1) * chunkSize; x++)
        {
            for (int y = yCord * chunkSize; y < (yCord + 1) * chunkSize; y++)
            {
                Vector3Int cellPosition = tilemap.WorldToCell(new Vector3((x + y) / 2f, (x - y) / 4f, 0));

                tilemap.SetTile(cellPosition, null);
            }
        }
        loadedChunks.Remove(new Vector2Int(xCord, yCord));

        VegetationGenerator.unLoadChunkVegetation(xCord, yCord);
    }

    void GenerateChunk(int xCord, int yCord)
    {
        if (!loadedChunks.ContainsKey(new Vector2Int(xCord, yCord)))
        {
            for (int x = xCord * chunkSize; x < (xCord + 1) * chunkSize; x++)
            {
                for (int y = yCord * chunkSize; y < (yCord + 1) * chunkSize; y++)
                {
                    Vector3Int cellPosition = tilemap.WorldToCell(new Vector3((x + y) / 2f, (x - y) / 4f, 0));

                    if (PerlinNoise.GeneratePerlinNoiseValue(cellPosition.x, cellPosition.y) <= 0.5f)
                    {
                        tilemap.SetTile(cellPosition, tiles[0]);
                    }
                    else
                    {
                        tilemap.SetTile(cellPosition, tiles[2]);
                    }

                }
            }
            loadedChunks.Add(new Vector2Int(xCord, yCord), true);
        }
        VegetationGenerator.checkChunk(xCord, yCord);
    }

    void CheckIfChunkNeedsToBeLoaded(int xCord, int yCord)
    {
        List<Vector2Int> unloadedChunks = new List<Vector2Int>();

        foreach (Vector2Int chunkCoords in loadedChunks.Keys)
        {
            int xpos = chunkCoords.x;
            int ypos = chunkCoords.y;

            bool chunkUnLoad = true;


            for (int x = xCord - 1; x <= xCord + 1; x++)
            {
                for (int y = yCord - 1; y <= yCord + 1; y++)
                {

                    if (xpos == x && ypos == y)
                    {

                        chunkUnLoad = false;
                        break;
                    }
                }
            }
            if (chunkUnLoad)
            {
                unloadedChunks.Add(new Vector2Int(xpos, ypos));
            }
        }

        foreach (Vector2Int chunks in unloadedChunks)
        {
            int x = chunks.x;
            int y = chunks.y;
            unLoadChunk(x, y);
        }

        unloadedChunks.Clear();

    }

    public int getChunkSize(){
        return chunkSize;
    }

}



