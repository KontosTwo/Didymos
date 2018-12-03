using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using System.Reflection;
using Environment;
using System;



public class Grid : MonoBehaviour
{
    private MapNode[,] nodes;

    [SerializeField]
    private MapBound mapbounds;

    [SerializeField]
    private float nodeSize;
    private Vector2 bottomLeftCorner;
    private Vector2 dimensions;

    void Awake()
    {
        Vector3 bottomLeftCorner3d = mapbounds.GetBottomLeftCorner();
        Vector3 dimensions3d = mapbounds.GetDimensions();
        bottomLeftCorner = new Vector2(bottomLeftCorner3d.x, bottomLeftCorner3d.z);
        dimensions = new Vector2(dimensions3d.x, dimensions3d.z);
        nodes = new MapNode[(int)(dimensions.x / nodeSize), (int)(dimensions.y / nodeSize)];
    }

    void Start()
    {
        for (int i = 0; i < nodes.GetLength(0); i++)
        {
            for (int j = 0; j < nodes.GetLength(1); j++)
            {
                Vector3 worldLocation = NodeToWorldCoord(new Point(i, j));
                nodes[i, j] = EnvironmentPhysics.CreateMapNoteAt(worldLocation.x, worldLocation.z);
            }
        }
    }

    public int DistanceToNodeDistance(float distance){
        return (int)(distance / nodeSize);
    }

    void Update()
    {

    }

    /*void OnDrawGizmos(){
        if(nodes != null){
            for(int i = 0; i < nodes.GetLength(0); i ++){
                for(int j = 0; j < nodes.GetLength(1); j ++){
                    Node node = nodes [i, j];
                    Gizmos.color = (node.terrainIsWalkable)?Gizmos.color:Color.red;
                    //Gizmos.DrawCube(NodeToWorldCoord(new Point(i,j)), Vector3.one * (nodeSize-.1f));
                }
            }
        }
    }*/



    public Point WorldCoordToNode(Vector3 worldCoord)
    {
        return new Point((int)(worldCoord.x / nodeSize - bottomLeftCorner.x), (int)(worldCoord.z / nodeSize - bottomLeftCorner.y));
    }

    public Vector3 NodeToWorldCoord(Point point)
    {
        return new Vector3(
            (point.x * nodeSize + nodeSize / 2) + bottomLeftCorner.x
            , nodes[point.x, point.y].height
            , (point.y * nodeSize + nodeSize / 2) + bottomLeftCorner.y);
    }

    public MapNode GetNodeAt(Point point)
    {
        return nodes[point.x, point.y];
    }

    public List<Point> GetNeighbors(Point point)
    {
        List<Point> neighbors = new List<Point>();
        Point p1 = new Point(point.x - 1, point.y - 1);
        if (InBound(p1))
        {
            neighbors.Add(p1);
        }
        Point p2 = new Point(point.x - 1, point.y);
        if (InBound(p2))
        {
            neighbors.Add(p2);
        }
        Point p3 = new Point(point.x - 1, point.y + 1);
        if (InBound(p3))
        {
            neighbors.Add(p3);
        }
        Point p4 = new Point(point.x, point.y + 1);
        if (InBound(p4))
        {
            neighbors.Add(p4);
        }
        Point p5 = new Point(point.x + 1, point.y + 1);
        if (InBound(p5))
        {
            neighbors.Add(p5);
        }
        Point p6 = new Point(point.x + 1, point.y);
        if (InBound(p6))
        {
            neighbors.Add(p6);
        }
        Point p7 = new Point(point.x + 1, point.y - 1);
        if (InBound(p7))
        {
            neighbors.Add(p7);
        }
        Point p8 = new Point(point.x, point.y - 1);
        if (InBound(p8))
        {
            neighbors.Add(p8);
        }
        return neighbors;
    }
    /*
     * Optimize by using HashSet?
     */
    public List<Point> GetNeighbors(Point point, int radius){
        List<Point> ret = new List<Point>();
        int originX = point.x;
        int originY = point.y;
        int minY = Mathf.Clamp(originY - radius, 0, nodes.GetLength(0) - 1);
        int maxY = Mathf.Clamp(originY + radius, 0, nodes.GetLength(0) - 1);
        int minX = Mathf.Clamp(originX - radius, 0, nodes.GetLength(1) - 1);
        int maxX = Mathf.Clamp(originX + radius, 0, nodes.GetLength(1) - 1);

        for (int y = minY; y < maxY; y++)
        {
            for (int x = minX; x < maxX; x++)
            {
                if (x != originX && y != originY){
                    ret.Add(new Point(x, y));
                }
            }
        }
        return ret;
    }
    private bool InBound(Point p)
    {
        return p.x >= 0 && p.x < nodes.GetLength(0) - 1 && p.y >= 0 && p.y < nodes.GetLength(1);
    }


    // change to class once size exceeds 16 bytes

}


public struct MapNode
{
    public MapNode(float height, bool walkable)
    {
        this.height = height;
        terrainIsWalkable = walkable;
    }
    public readonly float height;
    public readonly bool terrainIsWalkable;
}