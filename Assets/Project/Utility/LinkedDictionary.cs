using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
/*
 * Source (Copied)
 * https://stackoverflow.com/questions/29205934/c-sharp-equivalent-of-linkedhashmap
 */
using UnityEngine.Profiling;
public class LinkedDictionary<T, U> : IEnumerable<KeyValuePair<long,Tuple<U,T>>> where U : class{
    private Dictionary<T, LinkedListNode<Tuple<long, Tuple<U, T>>>> D;
    private SortedList<long,Tuple<U, T>> LL;
    /*
     * LinkedDictionary will malfunction
     * when overflow occurs. Don't overuse.    
     */
    private long tail;
    private long head;

    public LinkedDictionary(){
        D = new Dictionary<T, LinkedListNode<Tuple<long,Tuple<U, T>>>>();
        LL = new SortedList<long, Tuple<U, T>>();
        head = 0;
        tail = 0;
    }

    public U this[T c]{
        get{
            LinkedListNode<Tuple<long, Tuple<U, T>>> value = null;
            if(D.TryGetValue(c,out value)){
                return D[c].Value.Item2.Item1;
            }
            else{
                return null;
            }
        }

        set{
            if (D.ContainsKey(c)){
                LL.Remove(D[c].Value.Item1);
            }

            D[c] = 
                new LinkedListNode<Tuple<long,Tuple<U, T>>>(
                    new Tuple<long,Tuple<U,T>>(
                        tail,
                        new Tuple<U, T>(value, c)
                    )
                );
            LL.Add(D[c].Value.Item1,D[c].Value.Item2);
            tail++;
        }
    }

    public bool ContainsKey(T k){
        return D.ContainsKey(k);
    }

    public U PopFirst(){
        long index = head;
        var node = LL[index];
        LL.Remove(index);
        D.Remove(node.Item2);
        head++;
        return node.Item1;
    }

    public U PopLast(){
        long index = tail - 1;
        var node = LL[index];
        LL.Remove(index);
        D.Remove(node.Item2);
        tail--;
        return node.Item1;
    }

    public IEnumerator<KeyValuePair<long,Tuple<U,T>>> GetEnumerator(){
        return LL.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return LL.GetEnumerator();
    }

    public int Count{
        get{
            return D.Count;
        }
    }

  
}
