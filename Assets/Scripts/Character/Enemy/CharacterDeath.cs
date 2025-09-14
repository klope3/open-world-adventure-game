using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class CharacterDeath : MonoBehaviour
{
    [SerializeField, Sirenix.OdinInspector.InfoBox("This script handles common death behaviors for characters. Also 'resets' the changes on respawn. Attach to the top-level GameObject. Any of these fields can be left empty if unneeded.")] 
    private Character character;
    [SerializeField] private HealthHandler healthHandler;
    [SerializeField] private TargetablePoint targetablePoint;
    [SerializeField] private ParticleSystem[] particleSystems;
    [SerializeField] private Collider characterCollider;
    [SerializeField] private SkinnedMeshRenderer[] meshesToHide;
    [SerializeField] private Animator animator;
    [SerializeField] private Animancer.AnimancerComponent animancer;
    [SerializeField] private DamageZone damageZone;
    [SerializeField] private EnemyBasicStateManager stateManager;

    public void DoDeath()
    {
        //if (character) character.enabled = false;
        if (characterCollider) characterCollider.enabled = false;
        if (targetablePoint) targetablePoint.gameObject.SetActive(false);
        if (animator) animator.enabled = false;
        if (animancer != null) animancer.Stop();
        if (stateManager) stateManager.enabled = false;
        if (damageZone) damageZone.gameObject.SetActive(false);
        
        if (meshesToHide != null)
        {
            for (int i = 0; i < meshesToHide.Length; i++)
            {
                meshesToHide[i].gameObject.SetActive(false);
            }
        }

        if (particleSystems != null)
        {
            for (int i = 0; i < particleSystems.Length; i++)
            {
                particleSystems[i].Play();
            }
        }
    }

    private void OnEnable()
    {
        //if (character) character.enabled = true;
        if (targetablePoint) targetablePoint.gameObject.SetActive(true);
        if (characterCollider) characterCollider.enabled = true;
        if (animator) animator.enabled = true;
        if (stateManager) stateManager.enabled = true;
        if (healthHandler) healthHandler.Initialize();

        if (meshesToHide != null)
        {
            for (int i = 0; i < meshesToHide.Length; i++)
            {
                meshesToHide[i].gameObject.SetActive(true);
            }
        }
    }
}
