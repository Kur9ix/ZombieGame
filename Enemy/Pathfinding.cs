using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinding : MonoBehaviour
{
    public Tilemap grid;

    public GameObject obstacleCheckGameObject;

    public List<NodeBase> tosearch = new List<NodeBase>();

    public List<NodeBase> searched = new List<NodeBase>();

    public int newCost;
    public NodeBase node;
    public NodeBase startPoint;
    public NodeBase targetPoint;

    public List<Vector3Int> pathfindingPoints(Vector3 startPos, Vector3 targetPos)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        startPoint = new NodeBase(grid.WorldToCell(new Vector3((startPos.x + startPos.y) / 2f, (startPos.x - startPos.y) / 4f, 0)));
        targetPoint = new NodeBase(grid.WorldToCell(new Vector3((targetPos.x + targetPos.y) / 2f, (targetPos.x - targetPos.y) / 4f, 0)));

        //getNeighbours(startPoint.position);
        tosearch.Add(startPoint);

        while (tosearch.Count > 0)
        {
            node = tosearch[0];
            for (int i = 0; i < tosearch.Count; i++)
            {

                //print(tosearch[i].FCost);
                if (tosearch[i].FCost <= node.FCost)
                {

                    if (tosearch[i].hCost < node.hCost)
                    {
                        node = tosearch[i];
                    }
                }
            }

            searched.Add(node);
            tosearch.Remove(node);

            if (Vector3.Distance(node.position, targetPoint.position) < 0.2f)
            {
                Debug.LogError("TESTTSTSS");
                break;
            }

            foreach (Vector3Int vector in getNeighbours(node.position))
            {
                NodeBase temp = new NodeBase(vector);
                temp.parent = node;
                temp.gCost = node.gCost + CalculateGCost(node.position, temp.position);
                temp.hCost = CalculateHCost(temp.position, targetPoint.position);

                if (!obstacleCheck(vector))
                {
                    if (!checkIfExits(temp.position, searched))
                    {

                        if (!checkIfExits(temp.position, tosearch) || temp.gCost < node.gCost)
                        {

                            tosearch.Add(temp);

                        }
                    }
                }
            }


        }

        targetPoint.parent = node.parent;
        return getFinalPath();

    }

    bool checkIfExits(Vector3Int vector, List<NodeBase> list)
    {
        foreach (NodeBase node in list)
        {
            if (node.position == vector)
            {
                return true;
            }
        }
        return false;
    }

    public List<Vector3Int> getFinalPath()
    {
        List<Vector3Int> finalPath = new List<Vector3Int>();

        NodeBase currentNode = targetPoint;

        while (currentNode != null)
        {
            finalPath.Add(currentNode.position);

            currentNode = currentNode.parent;
        }
        finalPath.Reverse();
        return finalPath;
    }


    List<Vector3Int> getNeighbours(Vector3Int pos)
    {
        List<Vector3Int> neighbours = new List<Vector3Int>();
        for (int x = pos.x - 1; x <= pos.x + 1; x++)
        {
            for (int y = pos.y - 1; y <= pos.y + 1; y++)
            {
                Vector3Int cellPosition = grid.WorldToCell(new Vector3((x + y) / 2f, (x - y) / 4f, 0));
                if (cellPosition != pos)
                {
                    neighbours.Add(cellPosition);
                }

            }
        }

        return neighbours;
    }

    bool obstacleCheck(Vector3Int pos)
    {
        obstacleCheckGameObject.SetActive(true);
        obstacleCheckGameObject.transform.position = pos;
        bool check = obstacleCheckGameObject.GetComponent<ObstacalCheck>();
        obstacleCheckGameObject.SetActive(false);
        return false;  // check dosent work  
    }

    int CalculateGCost(Vector3Int currentPos, Vector3Int neighborPos)
    {
        int dx = Mathf.Abs(currentPos.x - neighborPos.x);
        int dy = Mathf.Abs(currentPos.y - neighborPos.y);
        if (dx > dy){
            return 14 * dy + 10 * (dx - dy);
        }
        return 14 * dx + 10 * (dy - dx);
    }


    float CalculateHCost(Vector3Int currentPos, Vector3Int targetPos)
    {
        int dx = Mathf.Abs(currentPos.x - targetPos.x);
        int dy = Mathf.Abs(currentPos.y - targetPos.y);
        return Mathf.Sqrt(dx * dx + dy * dy);
    }



}

