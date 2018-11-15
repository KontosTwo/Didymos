using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
 * Source (Paraphrased)
 * http://gameprogrammingpatterns.com/object-pool.html
 */
public class Pool<P> where P : Poolable<P>, new()
{
    private P[] pool;
    private P available;

    private static readonly float EXP_INCREASE = 1.4f;

    public Pool(int initialSize){
        pool = new P[initialSize];
        for (int i = 0; i < pool.Length; i ++){
            pool[i] = new P();
        }
        for (int i = 0; i < pool.Length - 1; i++){
            pool[i].SetNext(pool[i + 1]);
        }
        available = pool[0];
    }

    public P Get(){
        if(available.GetNext() == null){
            Resize();
        }
        P ret = available;
        available = available.GetNext();
        return ret;
    }

    public void Recycle(P used){
        used.SetNext(available);
        available = used;
    }

    private void Resize(){
        P[] newPool = new P[(int)(pool.Length * EXP_INCREASE)];
        for (int i = 0; i < pool.Length; i ++){
            newPool[i] = pool[i];
        }
        for (int i = pool.Length; i < newPool.Length; i ++){
            newPool[i] = new P();
        }
        available.SetNext(newPool[pool.Length]);
        for (int i = pool.Length; i < newPool.Length - 1; i++){
            newPool[i].SetNext(newPool[i + 1]);
        }
        pool = newPool;
    }
}
public interface Poolable<P>
{
    P GetNext();
    void SetNext(P next);
}