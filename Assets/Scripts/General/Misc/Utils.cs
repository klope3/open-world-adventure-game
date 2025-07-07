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

    /// <summary>
    /// Calculate how "in front of" referenceTransform the otherTransform is.
    /// If 1, referenceTransform is perfectly aimed at otherTransform (i.e. otherTransform is perfectly "in front").
    /// If -1, referenceTransform is perfectly aimed away from otherTransform (i.e. otherTransform is perfectly "behind").
    /// </summary>
    /// <param name="referenceTransform"></param>
    /// <param name="otherTransform"></param>
    /// <returns></returns>
    public static float CalculateInFrontFactor(Transform referenceTransform, Transform otherTransform, bool flattenHeight = false)
    {
        Vector3 referencePosition = flattenHeight ? new Vector3(referenceTransform.position.x, 0, referenceTransform.position.z) : referenceTransform.position;
        Vector3 otherPosition = flattenHeight ? new Vector3(otherTransform.position.x, 0, otherTransform.position.z) : otherTransform.position;
        Vector3 vecToOther = otherPosition - referencePosition;
        return Vector3.Dot(referenceTransform.forward, vecToOther.normalized);
    }

    /// <summary>
    /// Calculate how "on the right of" referenceTransform the otherTransform is.
    /// If 1, referenceTransform is perfectly "on the right of" otherTransform.
    /// If -1, referenceTransform is perfectly "on the left of" otherTransform.
    /// </summary>
    /// <param name="referenceTransform"></param>
    /// <param name="otherTransform"></param>
    /// <returns></returns>
    public static float CalculateOnRightFactor(Transform referenceTransform, Transform otherTransform)
    {
        Vector3 vecToOther = otherTransform.position - referenceTransform.position;
        return Vector3.Dot(referenceTransform.right, vecToOther.normalized);
    }

    /// <summary>
    /// Calculate how "above" referenceTransform the otherTransform is.
    /// If 1, referenceTransform is perfectly "above" otherTransform.
    /// If -1, referenceTransform is perfectly "below" otherTransform.
    /// Height is irrelevant in this calculation.
    /// </summary>
    /// <param name="referenceTransform"></param>
    /// <param name="otherTransform"></param>
    /// <returns></returns>
    public static float CalculateAboveFactor(Transform referenceTransform, Transform otherTransform)
    {
        Vector3 vecToOther = otherTransform.position - referenceTransform.position;
        return Vector3.Dot(referenceTransform.up, vecToOther.normalized);
    }
}
