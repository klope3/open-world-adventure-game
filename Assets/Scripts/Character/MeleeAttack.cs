using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private ECM2CharacterAdapter characterAdapter;
    [SerializeField] private Collider meleeZone;
    [SerializeField] private float meleeZoneActiveTime;
    [SerializeField] private float attacksPerSecond;
    [SerializeField] private float stopMovementTime;
    private float timer;
    private float timerMax;
    public System.Action OnAttack;

    private void Awake()
    {
        timerMax = 1 / attacksPerSecond;
    }

    private void Update()
    {
        //temp input
        if (Input.GetKeyDown(KeyCode.LeftControl) && timer >= timerMax && character.IsGrounded())
        {
            DoAttack();
        }

        if (timer < timerMax) timer += Time.deltaTime;
    }

    public void DoAttack()
    {
        StopCoroutine(CO_Attack());
        StartCoroutine(CO_Attack());
        timer = 0;
        OnAttack?.Invoke();
    }

    private IEnumerator CO_Attack()
    {
        meleeZone.gameObject.SetActive(true);
        characterAdapter.canMove = false;
        character.canEverJump = false;
        yield return new WaitForSeconds(meleeZoneActiveTime);
        meleeZone.gameObject.SetActive(false);

        yield return new WaitForSeconds(stopMovementTime - meleeZoneActiveTime);
        characterAdapter.canMove = true;
        character.canEverJump = true;
    }
}
