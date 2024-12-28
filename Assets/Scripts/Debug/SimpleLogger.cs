using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Easily make Debug.Log calls in Unity via Unity Events. Saves times in recompiles due to adding/removing these calls in the code.
public class SimpleLogger : MonoBehaviour
{
    public void DebugLog(string message)
    {
        Debug.Log(message);
    }
}
