using System;
using UnityEngine;
public class MapNode
{
    private readonly float height;
    private readonly bool terrainIsWalkable;
    private bool isCoverNode;
    private Vector3 location; 

    private static readonly float NEGLIGIBLE_COVER_THRESHOLD = 0.9f;

    public MapNode(Vector3 location,float height, bool walkable)
    {
        this.height = height;
        terrainIsWalkable = walkable;
        isCoverNode = false;
    }

    public void MarkAsCoverNode(){
        isCoverNode = true;
    }

    public Vector3 GetLocation(){
        return location;
    }

    public float GetHeight(){
        return location.y;
    }

    public bool TerrainIsWalkable(){
        return terrainIsWalkable;
    }

    public bool LowerThan(MapNode other){
        return other.height - this.height > NEGLIGIBLE_COVER_THRESHOLD;
    }

    public bool IsCoverNode(){
        return isCoverNode;
    }

}