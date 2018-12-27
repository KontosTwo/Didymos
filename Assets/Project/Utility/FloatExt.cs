using System;
public static class FloatExt
{

    public static bool IsBetween(this float x, float a, float b)
    {
        return (x - a) * (x - b) < 0;
    }

    public static bool CloseToZero(this float x,float marginOfError){
        return Math.Abs(x) < marginOfError;
    }

}

