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
    private Dictionary<T, PoolableLLNode<Tuple<U, T>>> D;
    private LinkedList<Tuple<U, T>> LL;

    private Pool<PoolableLLNode<Tuple<U, T>>> nodePool;


    public LinkedDictionary(){
        D = new Dictionary<T, PoolableLLNode<Tuple<U, T>>>();
        LL = new LinkedList<Tuple<U, T>>();
        nodePool = new Pool<PoolableLLNode<Tuple<U, T>>>(20);
    }

    public U this[T c]
    {
        get
        {
            return D[c].node.Value.Item1;
        }

        set
        {
            if (D.ContainsKey(c))
            {
                LL.Remove(D[c].node);
            }

            D[c] = new PoolableLLNode<Tuple<U, T>>(Tuple<U,T>.Create(value, c));
            LL.AddLast(D[c].node);
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

    public U PopLast()
    {
        var node = LL.Last;
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

        public Tuple(){
            
        }

        public Tuple(U i1,T i2){
            Item1 = i1;
            Item2 = i2;
        }

        public static Tuple<U,T> Create(U i1,T i2){
            return new Tuple<U,T>(i1, i2);
        }
    }

    private class PoolableLLNode<T> : Poolable<PoolableLLNode<T>> where T: new(){
        public LinkedListNode<T> node;

        public PoolableLLNode(T data){
            node = new LinkedListNode<T>(data);
        }

        public PoolableLLNode(){
            node = new LinkedListNode<T>(new T());
        }
    }
}
