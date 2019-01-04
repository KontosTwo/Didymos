using System;
using UnityEngine;
using System.Collections.Generic;
public class MapNode{
    /*
     * From top to bottom
     */
    private List<Tuple<float, Obstacle>> layers;
    private readonly float height;
    private readonly bool terrainIsWalkable;
    private bool isCoverNode;
    private Vector3 location;
    private bool neighboursChecked;

    private static readonly float NEGLIGIBLE_COVER_THRESHOLD = 0.9f;

    public MapNode(
        Vector3 location,
        float height,
        List<Tuple<float, Obstacle>> layers,
        bool walkable
    ){
        this.height = height;
        terrainIsWalkable = walkable;
        isCoverNode = false;
        this.location = location;
        neighboursChecked = false;
        this.layers = layers;
    }

    public bool AdjacencyDataSet(){
        //CheckIfNeighborsCalculated();

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
        //CheckIfNeighborsCalculated();

        return location;
    }

    public float GetHeight(){
        //CheckIfNeighborsCalculated();

        return location.y;
    }

    public bool TerrainIsWalkable(){
        //CheckIfNeighborsCalculated();

        return terrainIsWalkable;
    }
    public bool IsCoverNode(){
        CheckIfNeighborsCalculated();
        return isCoverNode;
    }
    private void MarkAsCoverNode(){
        isCoverNode = true;
    }


    private bool LowerThan(MapNode other){
        return other.height - this.height
            > NEGLIGIBLE_COVER_THRESHOLD;
    }

    private void CheckIfNeighborsCalculated(){
        if (!neighboursChecked){
            Debug.LogError("WARNING: This node has not been fully calculated yet");
        }
    }
}