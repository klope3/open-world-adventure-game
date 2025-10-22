using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a temp script to fake carrying behavior in the interest of showcasing it for a trailer
public class TEMP_CarryScript : MonoBehaviour
{
    [SerializeField] private PlayerStateManager playerStateManager;
    [SerializeField] private GameObject swordObject;
    [SerializeField] private GameObject shieldObject;
    [SerializeField] private GameObject carriedObject;

    [Sirenix.OdinInspector.Button]
    public void Activate()
    {
        playerStateManager.trigger = "carry";
        swordObject.SetActive(false);
        shieldObject.SetActive(false);
        carriedObject.SetActive(true);
    }
}
