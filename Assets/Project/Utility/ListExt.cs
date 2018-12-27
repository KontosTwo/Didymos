using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public static class ListExt{
   public static T MinBy<T,C>(this List<T> list, Func<T, C> transformer) where C:IComparable<C> {
        C min = list.Min(transformer);
        Dictionary<C, T> lookup = new Dictionary<C, T>();
        list.ForEach(t =>{
            lookup.Add(transformer(t), t);
        });
        return lookup[min];
   }
    public static T MaxBy<T, C>(this List<T> list, Func<T, C> transformer) where C : IComparable<C>{
        C max = list.Max(transformer);
        Dictionary<C, T> lookup = new Dictionary<C, T>();
        list.ForEach(t => {
            lookup.Add(transformer(t), t);
        });
        return lookup[max];
    }
}

