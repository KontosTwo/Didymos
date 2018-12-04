using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathfinderNode : IHeapItem<PathfinderNode>
{
    private Point location;
    private MapNode data;


    public int gCost;
    public int hCost;
    public int strategyCost;
    private PathfinderNode parent;
    int heapIndex;

    public PathfinderNode(Point gridLocation,
                          MapNode data)
    {
        location = gridLocation;
        this.data = data;
        strategyCost = 0;
    }


    public Point GetGridCoord()
    {
        return location;
    }

    public bool isWalkable()
    {
        return data.terrainIsWalkable;
    }

    public bool IsCover(){
        return data.IsCoverNode();
    }

    public float GetHeight()
    {
        return data.height;
    }

    public void SetParent(PathfinderNode p)
    {
        this.parent = p;
    }

    public List<PathfinderNode> TraceParents(PathfinderNode startNode)
    {
        List<PathfinderNode> path = new List<PathfinderNode>();
        PathfinderNode currentNode = this;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Add(startNode);

        return path;
    }


    public int FCost
    {
        get
        {
            return gCost + hCost + strategyCost;
        }
    }

    public bool WithInRangeOfStart(int manhattanGridDist)
    {
        return DistanceCost < manhattanGridDist;
    }

    public int DistanceCost
    {
        get
        {
            return  gCost;
        }
    }

    private int ComparerCost{
        get{
            return strategyCost*hCost;
        }

    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(PathfinderNode nodeToCompare)
    {
        bool thisIsCover = IsCover();
        bool otherIsCover = nodeToCompare.IsCover();
        int compare = 0;
        if ((thisIsCover && otherIsCover) || (!thisIsCover && !otherIsCover)){
            compare = ComparerCost.CompareTo(nodeToCompare.ComparerCost);
            if (compare == 0)
            {
                compare = (hCost).CompareTo(nodeToCompare.hCost);
            }
        }
        else if(thisIsCover){
            compare =  -1;
        }else if(otherIsCover){

            compare =  1;
        }




        return -compare;
    }
}