using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class NodeBase
{
    public Vector3Int position;
    public int gCost;
    public float hCost;
    public float FCost { get { return gCost + hCost; } }
    public NodeBase parent;

    public NodeBase(Vector3Int _position)
    {
        position = _position;
    }
}