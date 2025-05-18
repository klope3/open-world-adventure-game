using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class LadderBuilder : MonoBehaviour
{
    [InfoBox("Use the ladder builder to easily build correct ladders without manually placing each segment. DO NOT add any extra GameObjects as children of this one, or the builder won't work correctly.")]
    [SerializeField] private GameObject ladderPf;
    private static readonly float SEGMENT_SIZE = 0.8f;

    [Button, HideInPlayMode]
    public void AddSegment()
    {
        if (transform.childCount == 0)
        {
            Instantiate(ladderPf, transform);
            return;
        }

        Transform lastSegment = transform.GetChild(transform.childCount - 1);
        Vector3 newSegmentPosition = lastSegment.position + Vector3.up * SEGMENT_SIZE;
        GameObject newSegment = Instantiate(ladderPf, transform);
        newSegment.transform.position = newSegmentPosition;
    }

    [Button, HideInPlayMode]
    public void RemoveSegment()
    {
        if (transform.childCount == 0) return;

        Transform lastSegment = transform.GetChild(transform.childCount - 1);
        DestroyImmediate(lastSegment.gameObject);
    }
}
