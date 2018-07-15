using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CircularLinkedList<T>
{
    private List<T> data;
    private int index;

    public CircularLinkedList(List<T> data)
    {
        index = 0;
        this.data = data;
    }

    public void ShiftRight(){
        index = (index + 1) % data.Count;
    }
    public void ShiftLeft()
    {
        index--;
        if(index < 0){
            index = data.Count - 1;
        }
    }
    public T Get(){
        return data[index];
    }
}

