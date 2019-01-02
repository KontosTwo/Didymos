using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class GridData {
    private Dictionary<Point, Tuple<MapNode, Point>> dict;
    private List<Tuple<MapNode, Point>> list;

    private Pool<Tuple<MapNode, Point>> entryPool;

    public GridData(int estimatedCapacity)
    {
        dict = new Dictionary<Point, Tuple<MapNode, Point>>();
        list = new List<Tuple<MapNode, Point>>();
        entryPool = new Pool<Tuple<MapNode, Point>>(estimatedCapacity);
    }

    public MapNode this[Point c]
    {
        get
        {
            Tuple<MapNode, Point> value = null;
            dict.TryGetValue(c, out value);
            return value == null ? null : value.Item1;
        }

        set
        {
            if (dict.ContainsKey(c))
            {
                list.Remove(dict[c]);
                entryPool.Recycle(dict[c]);
            }

            dict[c] = entryPool.Get();
            dict[c].Item1 = value;
            dict[c].Item2 = c;
            list.Add(dict[c]);
        }
    }

    public bool ContainsKey(Point k)
    {
        return dict.ContainsKey(k);
    }

    public MapNode EvictFirst()
    {
        var node = list[0];
        list.Remove(node);
        dict.Remove(node.Item2);
        entryPool.Recycle(node);
        return node.Item1;
    }

    public MapNode EvictLast()
    {
        var node = list[list.Count - 1];
        list.Remove(node);
        dict.Remove(node.Item2);
        entryPool.Recycle(node);
        return node.Item1;
    }

    public int Count
    {
        get
        {
            return dict.Count;
        }
    }

    private class Tuple<U, T> : Poolable<Tuple<U,T>>
    {
        public U Item1;
        public T Item2;

        public Tuple()
        {

        }

        public Tuple(U i1, T i2)
        {
            Item1 = i1;
            Item2 = i2;
        }

        public static Tuple<U, T> Create(U i1, T i2)
        {
            return new Tuple<U, T>(i1, i2);
        }
    }
}
*/