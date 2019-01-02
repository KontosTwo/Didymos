using System;
using UnityEngine;

public struct Point
{

    public readonly int x;
    public readonly int y;
    public Point(int x,int y){
		this.x = x;
		this.y = y;
	}
	public override string ToString(){
		return x + " " + y;
	}

    public override bool Equals(object obj)
    {
        Point other = (Point)obj;
        return other.x == this.x && other.y == this.y;
    }
    public override int GetHashCode()
    {
        int hash = 17;
        // Suitable nullity checks etc, of course :)
        hash = hash * 23 + x;
        hash = hash * 23 + y;
        return hash;
    }

    public Point Shift(int x,int y){
        return new Point(this.x + x, this.y + y);
    }
}


