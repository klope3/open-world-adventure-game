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

    public void DoDeath()
    {
        character.enabled = false;
        targetablePoint.gameObject.SetActive(false);
        characterCollider.enabled = false;
        animator.enabled = false;
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
        targetablePoint.gameObject.SetActive(true);
        characterCollider.enabled = true;
        animator.enabled = true;
        healthHandler.Reinitialize();
        for (int i = 0; i < meshesToHide.Length; i++)
        {
            meshesToHide[i].gameObject.SetActive(true);
        }
    }
}
