using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Profiling;
/*
 * Source (Paraphrased)
 * http://gameprogrammingpatterns.com/object-pool.html
 */
using UnityEngine.Profiling;
public class Pool<P> where P : new()
{
    private Queue<P> freeList;
    private long total;
    //private HashSet<P> pinned;
    //private Queue<P> pinQueue;
    //private HashSet<P> noDuplicates;

    private float expIncrease = 1.4f;

    public Pool(int initialSize,float expIncrease){
        freeList = new Queue<P>(initialSize);
        total = initialSize;
        //pinned = new HashSet<P>();
        //pinQueue = new Queue<P>();
        for(int i = 0; i < initialSize; i++){
            P newP = new P();
            freeList.Enqueue(newP);
        }
        this.expIncrease = expIncrease;
        if((int)(initialSize * (expIncrease - 1)) < 1)
        {
            Debug.Log("WARNING, pool will never grow");
        }
        //noDuplicates = new HashSet<P>(new NoDuplicateByObjectReference());
    }

    /*
     * This field is NOT thread safe!
     */
    private List<P> toBeRemoved;

    public P Get(){
        //int pinQueueSize = pinQueue.Count;
        //for(int i = 0; i < pinQueueSize; i++){
        //    P current = pinQueue.Dequeue();
        //    if (pinned.Contains(current)){
        //        pinQueue.Enqueue(current);
        //    }
        //    else{
        //        freeList.Enqueue(current);
        //    }
        //}
        if (freeList.Count <= 1){
            Resize();
        }
        P taken = freeList.Dequeue();
        //noDuplicates.Remove(taken);
        return taken;
    }

    public void Recycle(P obj)
    {
        //if (pinned.Contains(obj)){
        //    pinQueue.Enqueue(obj);
        //}
        //else{
            //if (!noDuplicates.Contains(obj))
           // {
               // noDuplicates.Add(obj);
                freeList.Enqueue(obj);
        //}
        //}
    }
    /*
     * The poolable object may have
     * been readded to the free list,
     * but it still might be in use.
     * In that case, pinning prevents Get()
     * from returning the pinned poolable object
     */
    public void Pin(P obj)
    {
        //pinned.Add(obj);
    }
    public void Unpin(P obj)
    {
        //pinned.Remove(obj);
    }
    public bool IsPinned(P obj)
    {
        //return pinned.Contains(obj);
        return false;
    }
    public long GetSize()
    {
        return total;
    }

    public override string ToString()
    {
        return "FreeCount: " + freeList.Count;
                //+  " | PinCount: " + pinQueue.Count;
    }

    private void Resize()
    {
        long newTotal = (int)(total * expIncrease);
        Profiler.BeginSample("Resize pool");
        for (int i = 0; i < newTotal - total; i++){
            P newP = new P();
            freeList.Enqueue(newP);
            //noDuplicates.Add(newP);
        }
        Debug.Log("RESIZING: " + freeList.Peek().GetType() + " " + (newTotal - total));
        total = newTotal;
        Profiler.EndSample();
    }
}