using System;
public class MapNode
{
    public readonly float height;
    public readonly bool terrainIsWalkable;
    private bool isCoverNode;

    private static readonly float NEGLIGIBLE_COVER_THRESHOLD = 0.9f;

    public MapNode(float height, bool walkable)
    {
        this.height = height;
        terrainIsWalkable = walkable;
        isCoverNode = false;
    }

    public void MarkAsCoverNode()
    {
        isCoverNode = true;
    }

    public bool LowerThan(MapNode other){
        return other.height - this.height > NEGLIGIBLE_COVER_THRESHOLD;
    }

    public bool IsCoverNode(){
        return isCoverNode;
    }

}