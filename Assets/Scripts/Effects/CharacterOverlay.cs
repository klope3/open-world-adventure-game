using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOverlay : MonoBehaviour
{
    //[SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private SkinnedMeshRenderer[] meshRenderers;
    private float timer;
    private delegate Color ColorFunc(float t);
    private ColorFunc colorFunction;

    private readonly float PULSE_FREQUENCY = 15f;
    private readonly float PULSE_OFFSET = 0.3f;
    private readonly float PULSE_AMPLITUDE = 0.3f;
    private readonly float PULSE_SECONDS = 1.5f;
    private readonly float FLASH_SECONDS = 1f;
    private readonly float FLASH_SLOPE = -1f;
    private readonly float FLASH_OFFSET = 0.6f;

    private readonly int OVERLAY_ID = Shader.PropertyToID("_Overlay");

    private void Awake()
    {
        colorFunction = NullFunc;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            SkinnedMeshRenderer meshRenderer = meshRenderers[i];
            meshRenderer.material.SetColor(OVERLAY_ID, colorFunction(timer));
        }
    }

    public void DoDamagePulse()
    {
        timer = 0;
        colorFunction = DamagePulseFunc;
    }

    public void DoDamageFlash()
    {
        timer = 0;
        colorFunction = DamageFlashFunc;
    }

    private Color NullFunc(float t)
    {
        return Color.clear;
    }

    private Color DamagePulseFunc(float t)
    {
        if (t > PULSE_SECONDS) return Color.clear;

        float opacity = PULSE_AMPLITUDE * Mathf.Sin(t * PULSE_FREQUENCY) + PULSE_OFFSET;
        return new Color(1, 0, 0, opacity);
    }

    private Color DamageFlashFunc(float t)
    {
        if (t > FLASH_SECONDS) return Color.clear;

        float opacity = Mathf.Clamp(t * FLASH_SLOPE + FLASH_OFFSET, 0, 1);
        return new Color(1, 0, 0, opacity);
    }
}
