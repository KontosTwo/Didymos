using System;
using UnityEngine;
using System.Collections.Generic;
public class MapNode
{
    private readonly float height;
    private readonly bool terrainIsWalkable;
    private bool isCoverNode;
    private Vector3 location;
    private bool neighboursChecked;

    private static readonly float NEGLIGIBLE_COVER_THRESHOLD = 0.9f;

    public MapNode(
        Vector3 location,
        float height, 
        bool walkable
    ){
        this.height = height;
        terrainIsWalkable = walkable;
        isCoverNode = false;
        this.location = location;
        neighboursChecked = false;
    }

    public bool AdjacencyDataSet(){
        return neighboursChecked;
    }

    public void CalculateAdjancencyData(
        List<MapNode> neighbours
    ){
        foreach (MapNode n in neighbours){
            if (LowerThan(n)){
                MarkAsCoverNode();
                break;
            }
        }
        neighboursChecked = true;
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
    public bool IsCoverNode(){
        if (!neighboursChecked){
            Debug.Log("WARNING: This node has not been fully calculated yet");
        }
        return isCoverNode;
    }
    private void MarkAsCoverNode(){
        isCoverNode = true;
    }


    private bool LowerThan(MapNode other){
        return other.height - this.height
            > NEGLIGIBLE_COVER_THRESHOLD;
    }
}