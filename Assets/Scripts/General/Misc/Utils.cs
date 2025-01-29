using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    /// <summary>
    /// Approximates what a "circular" vector (such as from an analog stick) would be as a "square" vector (i.e. where a perfectly northeast input would be (1, 1), with a magnitude sqrt(2)).
    /// Couldn't figure out the actual conversion formula, but this is good enough for now.
    /// </summary>
    /// <param name="circularVector"></param>
    /// <returns></returns>
    public static Vector3 ApproximateSquareInputVector(Vector3 circularVector)
    {
        float x = circularVector.x == 0 ? float.Epsilon : circularVector.x;
        float heading = Mathf.Atan(circularVector.y / x);
        float multiplier = (Mathf.Sqrt(2) * -1 + 1) * Mathf.Abs(Mathf.Cos(heading * 2)) + Mathf.Sqrt(2);
        return circularVector * multiplier;
    }
}
