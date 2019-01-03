using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
/*
 * Source (Copied)
 * https://stackoverflow.com/questions/29205934/c-sharp-equivalent-of-linkedhashmap
 */
public class LinkedDictionary<T, U> : IEnumerable<Tuple<U,T>> where U : class{
    private Dictionary<T, LinkedListNode<Tuple<U, T>>> D;
    private LinkedList<Tuple<U, T>> LL;



    public LinkedDictionary(){
        D = new Dictionary<T, LinkedListNode<Tuple<U, T>>>();
        LL = new LinkedList<Tuple<U, T>>();
    }

    public U this[T c]{
        get{
            LinkedListNode<Tuple<U, T>> value = null;
            if(D.TryGetValue(c,out value)){
                return D[c].Value.Item1;
            }
            else{
                return null;
            }
        }

        set{
            if (D.ContainsKey(c)){
                LL.Remove(D[c].Value);
            }

            D[c] = new LinkedListNode<Tuple<U, T>>(new Tuple<U, T>(value, c));
            LL.AddLast(D[c].Value);
        }
    }

    public bool ContainsKey(T k){
        return D.ContainsKey(k);
    }

    public U PopFirst(){
        var node = LL.First;
        LL.Remove(node);
        D.Remove(node.Value.Item2);
        return node.Value.Item1;
    }

    public U PopLast(){
        var node = LL.Last;
        LL.Remove(node);
        D.Remove(node.Value.Item2);
        return node.Value.Item1;
    }

    public IEnumerator<Tuple<U, T>> GetEnumerator(){
        return LL.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator(){
        return LL.GetEnumerator();
    }



    public int Count{
        get{
            return D.Count;
        }
    }

  
}
