using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class MapNode : Poolable<MapNode>
{
    private static Pool<MapNode> pool;
    private static int ESTIMATED_POOL_SIZE = 400;

    private float height;
    private float speedModifier;
    private bool terrainIsWalkable;

    public MapNode(float height, float speedModifier, bool walkable)
    {
        this.height = height;
        this.speedModifier = speedModifier;
        terrainIsWalkable = walkable;
    }
    static MapNode(){
        pool = new Pool<MapNode>(ESTIMATED_POOL_SIZE);
    }

    public MapNode(){
        
    }

    public static MapNode GetMapNode(){
        return pool.Get();
    }

    public static void RecycleMapNode(MapNode node){
        pool.Recycle(node);
    }

    public void Reinitialize(float height, float speedModifier, bool walkable)
    {
        this.height = height;
        this.speedModifier = speedModifier;
        terrainIsWalkable = walkable;
    }
    public float GetHeight()
    {
        return height;
    }

    public float GetSpeedModifier()
    {
        return speedModifier;
    }

    public bool IsTerrainWalkable()
    {
        return terrainIsWalkable;
    }
}

*/