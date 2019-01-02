using System;
using UnityEngine;

public struct Point
{
	public Point(int x,int y){
		this.x = x;
		this.y = y;
	}
	public override string ToString(){
		return x + " " + y;
	}
	public readonly int x;
	public readonly int y;

    public override bool Equals(object obj)
    {
        Point other = (Point)obj;
        return other.x == this.x && other.y == this.y;
    }

    public Point Shift(int x,int y){
        return new Point(this.x + x, this.y + y);
    }
}


