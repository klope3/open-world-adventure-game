using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class SkeletonDeath : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private HealthHandler healthHandler;
    [SerializeField] private TargetablePoint targetablePoint;
    [SerializeField] private ParticleSystem[] particleSystems;
    [SerializeField] private Collider characterCollider;
    [SerializeField] private SkinnedMeshRenderer[] meshesToHide;
    [SerializeField] private Animator animator;
    [SerializeField] private DamageZone damageZone;
    [SerializeField] private EnemyStateManager stateManager;

    public void DoDeath()
    {
        character.enabled = false;
        characterCollider.enabled = false;
        animator.enabled = false;
        stateManager.enabled = false;
        damageZone.gameObject.SetActive(false);
        for (int i = 0; i < meshesToHide.Length; i++)
        {
            meshesToHide[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Play();
        }
    }

    private void OnEnable()
    {
        character.enabled = true;
        targetablePoint.SetIsTargetable(true);
        characterCollider.enabled = true;
        animator.enabled = true;
        stateManager.enabled = true;
        healthHandler.Reinitialize();
        for (int i = 0; i < meshesToHide.Length; i++)
        {
            meshesToHide[i].gameObject.SetActive(true);
        }
    }
}
