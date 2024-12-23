using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DirectionalInputProvider : MonoBehaviour
{
    public abstract Vector2 GetInput();
}
