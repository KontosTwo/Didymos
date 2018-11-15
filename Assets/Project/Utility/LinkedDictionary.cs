using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
/*
 * Source (Copied)
 * https://stackoverflow.com/questions/29205934/c-sharp-equivalent-of-linkedhashmap
 */
public class LinkedDictionary<T, U>
{
    Dictionary<T, LinkedListNode<Tuple<U, T>>> D = new Dictionary<T, LinkedListNode<Tuple<U, T>>>();
    LinkedList<Tuple<U, T>> LL = new LinkedList<Tuple<U, T>>();

    public U this[T c]
    {
        get
        {
            return D[c].Value.Item1;
        }

        set
        {
            if (D.ContainsKey(c))
            {
                LL.Remove(D[c]);
            }

            D[c] = new LinkedListNode<Tuple<U, T>>(Tuple<U,T>.Create(value, c));
            LL.AddLast(D[c]);
        }
    }

    public bool ContainsKey(T k)
    {
        return D.ContainsKey(k);
    }

    public U PopFirst()
    {
        var node = LL.First;
        LL.Remove(node);
        D.Remove(node.Value.Item2);
        return node.Value.Item1;
    }

    public int Count
    {
        get
        {
            return D.Count;
        }
    }

    private  class Tuple<U,T>{
        public U Item1;
        public T Item2;

        public Tuple(U i1,T i2){
            Item1 = i1;
            Item2 = i2;
        }

        public static Tuple<U,T> Create(U i1,T i2){
            return new Tuple<U,T>(i1, i2);
        }
    }
}
