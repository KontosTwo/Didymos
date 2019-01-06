using UnityEngine;
using System.Collections.Generic;
using static EnvironmentPhysics;
/*
 * If something wierd happens, blame
 * the incorrect recycling of lists 
 * and poolable objects
 */
using Unity.Jobs;
public class Pools : MonoBehaviour {
    private static Pools instance;

  


    private Pool<List<MapNode>> listMapNodes;
    private Pool<List<Point>> listPoints;
    private Pool<MapNode> mapNodes;
    private Pool<List<IntersectionResult>> listIntersectionResults;
    private Pool<HashSet<Point>> hashSetPoints;
    private Pool<Point> points;
    private Pool<List<Vector2>> listVector2s;
    private Pool<List<Vector3>> listVector3s;
    private Pool<List<FindTallEnoughDepthCubeJob>> listFindTallEnoughDepthCubeJob;
    private Pool<FindTallEnoughDepthCubeJob> findTallEnoughDepthCube;
    private Pool<List<JobHandle>> listJobHandles;

    private void Awake()
    {
        listMapNodes = new Pool<List<MapNode>>(100,1.4f);
        mapNodes = new Pool<MapNode>(1000,1.2f);
        listPoints = new Pool<List<Point>>(100,1.1f);
        listIntersectionResults = new Pool<List<IntersectionResult>>(30,1.3f);
        hashSetPoints = new Pool<HashSet<Point>>(10,1.5f);
        points = new Pool<Point>(1000,1.1f);
        listVector2s = new Pool<List<Vector2>>(10,1.5f);
        listVector3s = new Pool<List<Vector3>>(10,1.5f);
        listFindTallEnoughDepthCubeJob = new Pool<List<FindTallEnoughDepthCubeJob>>(3,1.5f);
        findTallEnoughDepthCube = new Pool<FindTallEnoughDepthCubeJob>(30, 1.5f);
        listJobHandles = new Pool<List<JobHandle>>(30, 1.5f);
        instance = this;
    }
    public static MapNode MapNode
    {
        get
        {
            MapNode data = instance.mapNodes.Get();
            data.Clear();
            return data;
        }
        set
        {
            instance.mapNodes.Recycle(value);
        }
    }
    public static Point Point
    {
        get
        {
            Point data = instance.points.Get();
            data.Clear();
            return data;
        }
        set
        {
            if (!Grid.PointInUse(value))
            {
                instance.points.Recycle(value);
            }
        }
    }
    public static List<MapNode> ListMapNodes
    {
        get
        {
            List<MapNode> mapnodes = 
                instance.listMapNodes.Get();
            mapnodes.Clear();
            return mapnodes;
        }
        set
        {
            instance.listMapNodes.Recycle(value);
        }
    }
    public static FindTallEnoughDepthCubeJob FindTallEnoughDepthCubeJob
    {
        get
        {
            FindTallEnoughDepthCubeJob data = instance.findTallEnoughDepthCube.Get();
            return data;
        }
        set
        {
            value.layers.Dispose();
            value.tallEnough.Dispose();
            instance.findTallEnoughDepthCube.Recycle(value);
        }
    }
    public static List<FindTallEnoughDepthCubeJob> ListFindTallEnoughDepthCubeJob
    {
        get
        {
            List<FindTallEnoughDepthCubeJob> data =
                instance.listFindTallEnoughDepthCubeJob.Get();
            data.Clear();
            return data;
        }
        set
        {
            instance.listFindTallEnoughDepthCubeJob.Recycle(value);
        }
    }
    public static List<JobHandle> ListJobHandles
    {
        get
        {
            List<JobHandle> data =
                instance.listJobHandles.Get();
            data.Clear();
            return data;
        }
        set
        {
            instance.listJobHandles.Recycle(value);
        }
    }
    public static void FreeListPoints(List<Point> data)
    {
        for(int i = 0; i < data.Count; i++){
            Point = data[i];
        }
    }
    public static void FreeHashSetPoints(HashSet<Point> data)
    {
        IEnumerator<Point> enumerator = data.GetEnumerator();
        while (enumerator.MoveNext())
        {
            Point = enumerator.Current;
        }
    }
    public static List<Point> ListPoints
    {
        get
        {
            List<Point> data =
                instance.listPoints.Get();
            data.Clear();
            return data;
        }
        set
        {
            instance.listPoints.Recycle(value);
        }
    }
    public static HashSet<Point> HashSetPoints
    {
        get
        {
            HashSet<Point> data =
                instance.hashSetPoints.Get();
            data.Clear();
            return data;
        }
        set
        {
            instance.hashSetPoints.Recycle(value);
        }
    }
    public static List<EnvironmentPhysics.IntersectionResult> ListIntersectionResults
    {
        get
        {
            List<EnvironmentPhysics.IntersectionResult> data =
                instance.listIntersectionResults.Get();
            data.Clear();
            return data;
        }
        set
        {
            instance.listIntersectionResults.Recycle(value);
        }
    }
    public static List<Vector2> ListVector2s
    {
        get
        {
            List<Vector2> data =
                instance.listVector2s.Get();
            data.Clear();
            return data;
        }
        set
        {
            instance.listVector2s.Recycle(value);
        }
    }

    public static List<Vector3> ListVector3s
    {
        get
        {
            List<Vector3> data =
                instance.listVector3s.Get();
            data.Clear();
            return data;
        }
        set
        {
            instance.listVector3s.Recycle(value);
        }
    }
   

    private void Update()
    {

    }

}
