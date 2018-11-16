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
	[SerializeField]
	private MapBound mapbounds;

	private Vector2 bottomLeftCorner;
	private Vector2 dimensions;
    private GridData sparseGrid;

    private static readonly float NODE_SIZE = 1.0f;
    private static readonly int MAX_SPARSE_GRID_SIZE = 2000;

	void Awake(){
		Vector3 bottomLeftCorner3d = mapbounds.GetBottomLeftCorner ();
		Vector3 dimensions3d = mapbounds.GetDimensions ();
		bottomLeftCorner = new Vector2 (bottomLeftCorner3d.x, bottomLeftCorner3d.z);
		dimensions = new Vector2 (dimensions3d.x, dimensions3d.z);
        sparseGrid = new GridData(MAX_SPARSE_GRID_SIZE);
	}

	void Start(){
		
	}

	void Update(){
		
	}

	public Point WorldCoordToNode(Vector3 worldCoord){
		return new Point ((int)(worldCoord.x / NODE_SIZE - bottomLeftCorner.x), (int)(worldCoord.z / NODE_SIZE - bottomLeftCorner.y));
	}
		
	public Vector3 NodeToWorldCoord(Point point){
        /*return new Vector3(
			(point.x * nodeSize  + nodeSize/2) + bottomLeftCorner.x
			,nodes[point.x,point.y].height
			,(point.y * nodeSize  + nodeSize/2) + bottomLeftCorner.y);*/
        return null;
	}

	public MapNode GetNodeAt(Point point){
        if(sparseGrid[point] == null){
            
        }
        return 0;
	}

	public List<Point> GetNeighbors(Point point){
		List<Point> neighbors = new List<Point> ();
		Point p1 = new Point (point.x - 1, point.y - 1);
		if(InBound(p1)){
			neighbors.Add (p1);
		}
		Point p2 = new Point (point.x - 1, point.y);
		if(InBound(p2)){
			neighbors.Add (p2);
		}
		Point p3 = new Point (point.x - 1, point.y + 1);
		if(InBound(p3)){
			neighbors.Add (p3);
		}
		Point p4 = new Point (point.x, point.y + 1);
		if(InBound(p4)){
			neighbors.Add (p4);
		}
		Point p5 = new Point (point.x + 1, point.y + 1);
		if(InBound(p5)){
			neighbors.Add (p5);
		}
		Point p6 = new Point (point.x + 1, point.y);
		if(InBound(p6)){
			neighbors.Add (p6);
		}
		Point p7 = new Point (point.x + 1, point.y - 1);
		if(InBound(p7)){
			neighbors.Add (p7);
		}
		Point p8 = new Point (point.x, point.y - 1);
		if(InBound(p8)){
			neighbors.Add (p8);
		}
		return neighbors;
	}
	private bool InBound(Point p){
		return p.x >= 0 && p.x < dimensions.x && p.y >= 0 && p.y < dimensions.y;
	}


	// change to class once size exceeds 16 bytes

}


