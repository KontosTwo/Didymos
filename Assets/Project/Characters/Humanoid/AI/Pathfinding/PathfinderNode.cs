using UnityEngine;
using System.Collections;



public class PathfinderNode : IHeapItem<PathfinderNode>
{
	private Point location;
	private MapNode data;

	public int movementPenalty;

	public int gCost;
	public int hCost;
    public int sCost;
	public PathfinderNode parent;
	int heapIndex;

	public PathfinderNode(Point gridLocation,MapNode data)
	{
		movementPenalty = 0;
		location = gridLocation;
		this.data = data;
        sCost = 0;
	}

	public Point GetGridCoord(){
		return location;
	}

	public bool isWalkable(){
        return data.terrainIsWalkable;
	}

	public float GetHeight(){
        return data.height;
	}

	public int fCost
	{
		get
		{
			return gCost + hCost + sCost;
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
		int compare = fCost.CompareTo(nodeToCompare.fCost);
		if (compare == 0)
		{
			compare = hCost.CompareTo(nodeToCompare.hCost);
		}
		return -compare;
	}

	public bool walkableTo(PathfinderNode other)
	{
		return true;
	}
}