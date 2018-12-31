using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathfinderNode : IHeapItem<PathfinderNode>
{
    private Point location;
    private MapNode data;
    private IComparer<PathfinderNode> comparer;
    private INodeDistanceClamper clamper;
    private IExtractCostFromCostResult extractor;

    private int physicalGCost;
    private int hCost;
    //Cost of moving from start to this node
    private int strategyGCost;
    //Cost of moving from previous to this node
    private CostResult strategyCost;
    //private int strategyCost;
    private PathfinderNode parent;
    private int heapIndex;

    public PathfinderNode(Point gridLocation,
                           MapNode data,
                           IComparer<PathfinderNode> comparer,
                           INodeDistanceClamper clamper,
                           IExtractCostFromCostResult extractor){
        location = gridLocation;
        this.data = data;
        this.comparer = comparer;
        this.clamper = clamper;
        this.extractor = extractor;
        physicalGCost = 0;
        hCost = 0;
        strategyGCost = 0;
    }
    public IExtractCostFromCostResult GetExtractor(){
        return extractor;
    }

    public Vector3 GetLocation(){
        return data.GetLocation();
    }

    public Point GetGridCoord(){
        return location;
    }

    public bool IsWalkable(){
        return data.TerrainIsWalkable();
    }

    public bool IsCover(){
        return data.IsCoverNode();
    }

    public float GetHeight(){
        return data.GetHeight();
    }

    public int GetGCost(){
        return GetPhysicalGCost() + GetStrategyGCost();
    }
    public int GetHCost(){
        return hCost;
    }
    public CostResult GetStrategyCost(){
        return strategyCost;
    }
    public void SetHCost(int hCost){
        this.hCost = hCost;
    }
    public void SetStrategyCost(
        CostResult strategyCost
    ){
        this.strategyCost = strategyCost;;
    }
    public void SetStrategyGCost(
        int accumulated
    ){
        this.strategyGCost = accumulated;
    }
   
    public void SetPhysicalGCost(int gCost){
        this.physicalGCost = gCost;
    }
    public int GetPhysicalGCost(){
        return physicalGCost;
    }

    public int GetStrategyGCost(){
       // Debug.Log(accumulatedStrategyCost);
        return strategyGCost;
    }

    public void SetParent(PathfinderNode p){
        this.parent = p;
    }

    public IComparer<PathfinderNode> GetComparer(){
        return comparer;
    }

    public List<PathfinderNode> TraceParents(PathfinderNode startNode){
        List<PathfinderNode> path = new List<PathfinderNode>();
        PathfinderNode currentNode = this;

        while (currentNode != startNode){
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Add(startNode);

        return path;
    }


    public int GetFCost()
    {
        return GetGCost() + hCost;
    }

    public bool WithInRangeOfStart(int manhattanGridDist)
    {
        return clamper.WithinRangeOfStart(
            this,
            manhattanGridDist
        );
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

    public int CompareTo(PathfinderNode nodeToCompare){
        return comparer.Compare(this, nodeToCompare);
    }
}