using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tries to make the GameObject it's attached to invisible on Awake.
/// Useful for objects that should always be visible in editor, but never in game (e.g. level transition triggers).
/// </summary>
public class HideOnAwake : MonoBehaviour
{
    private void Awake()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer) Destroy(renderer);
    }
}
