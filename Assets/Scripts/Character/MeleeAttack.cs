using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private GameObject meleeZone;

    public void OnFire()
    {
        StartCoroutine(CO_Attack());
    }

    private IEnumerator CO_Attack()
    {
        meleeZone.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        meleeZone.SetActive(false);
    }
}
