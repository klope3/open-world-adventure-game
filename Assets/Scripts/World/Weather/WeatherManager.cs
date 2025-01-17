using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] private EnvironmentSettingsSO morningColors;
    [SerializeField] private EnvironmentSettingsSO dayColors;
    [SerializeField] private EnvironmentSettingsSO eveningColors;
    [SerializeField] private EnvironmentSettingsSO nightColors;

    [SerializeField] private Light sunLight;
    [SerializeField] private MeshRenderer horizonMeshRenderer;
    [SerializeField] private MeshRenderer skyBaseMeshRenderer;
    [SerializeField] private MeshRenderer cloudsMeshRenderer;
    [SerializeField] private MeshRenderer sunInnerMeshRenderer;
    [SerializeField] private MeshRenderer sunOuterMeshRenderer;

    [SerializeField] private Transform sunMoonParent;

    [SerializeField] private float morningDuration;
    [SerializeField] private float dayDuration;
    [SerializeField] private float eveningDuration;
    [SerializeField] private float nightDuration;
    private float timer;

    public enum TimeOfDay
    {
        Morning,
        Day,
        Evening,
        Night
    }

    private void Update()
    {
        timer += Time.deltaTime;
        float nightEnd = morningDuration + dayDuration + eveningDuration + nightDuration;
        if (timer > nightEnd) timer = 0;

        PrepareLerp(out float lerpStartValue, out float lerpEndValue, out EnvironmentSettingsSO lerpStartColors, out EnvironmentSettingsSO lerpEndColors);
        ApplyLerp(lerpStartValue, lerpEndValue, lerpStartColors, lerpEndColors);
        RotateSunMoon();
    }

    private void CalculateValues(out float morningEnd, out float dayMidpoint, out float dayEnd, out float eveningEnd, out float nightMidpoint, out float nightEnd)
    {
        morningEnd = morningDuration;
        dayMidpoint = morningEnd + dayDuration / 2;
        dayEnd = dayMidpoint + dayDuration / 2;
        eveningEnd = dayEnd + eveningDuration;
        nightMidpoint = eveningEnd + nightDuration / 2;
        nightEnd = nightMidpoint + nightDuration / 2;
    }

    private void PrepareLerp(out float lerpStartValue, out float lerpEndValue, out EnvironmentSettingsSO lerpStartColors, out EnvironmentSettingsSO lerpEndColors)
    {
        CalculateValues(out float morningEnd, out float dayMidpoint, out float dayEnd, out float eveningEnd, out float nightMidpoint, out float nightEnd);

        if (timer >= 0 && timer < morningEnd)
        {
            lerpStartValue = 0;
            lerpEndValue = morningEnd;
            lerpStartColors = morningColors;
            lerpEndColors = dayColors;
        } else if (timer >= morningEnd && timer < dayMidpoint)
        {
            lerpStartValue = morningEnd;
            lerpEndValue = dayMidpoint;
            lerpStartColors = dayColors;
            lerpEndColors = dayColors;
        } else if (timer >= dayMidpoint && timer < dayEnd)
        {
            lerpStartValue = dayMidpoint;
            lerpEndValue = dayEnd;
            lerpStartColors = dayColors;
            lerpEndColors = eveningColors;
        } else if (timer >= dayEnd && timer < eveningEnd)
        {
            lerpStartValue = dayEnd;
            lerpEndValue = eveningEnd;
            lerpStartColors = eveningColors;
            lerpEndColors = nightColors;
        } else if (timer >= eveningEnd && timer < nightMidpoint)
        {
            lerpStartValue = eveningEnd;
            lerpEndValue = nightMidpoint;
            lerpStartColors = nightColors;
            lerpEndColors = nightColors;
        } else
        {
            lerpStartValue = nightMidpoint;
            lerpEndValue = nightEnd;
            lerpStartColors = nightColors;
            lerpEndColors = morningColors;
        }
    } 

    private void ApplyLerp(float lerpStartValue, float lerpEndValue, EnvironmentSettingsSO lerpStartSettings, EnvironmentSettingsSO lerpEndSettings)
    {
        float t = (timer - lerpStartValue) / (lerpEndValue - lerpStartValue);
        horizonMeshRenderer.material.color = Color.Lerp(lerpStartSettings.HorizonColor, lerpEndSettings.HorizonColor, t);
        skyBaseMeshRenderer.material.color = Color.Lerp(lerpStartSettings.ZenithColor, lerpEndSettings.ZenithColor, t);
        cloudsMeshRenderer.material.color = Color.Lerp(lerpStartSettings.CloudColor, lerpEndSettings.CloudColor, t);
        sunInnerMeshRenderer.material.color = Color.Lerp(lerpStartSettings.SunInnerColor, lerpEndSettings.SunInnerColor, t);
        sunOuterMeshRenderer.material.color = Color.Lerp(lerpStartSettings.SunOuterColor, lerpEndSettings.SunOuterColor, t);
        sunLight.color = Color.Lerp(lerpStartSettings.SunColor, lerpEndSettings.SunColor, t);
        RenderSettings.ambientLight = Color.Lerp(lerpStartSettings.AmbientLightColor, lerpEndSettings.AmbientLightColor, t);
        RenderSettings.fogColor = Color.Lerp(lerpStartSettings.FogColor, lerpEndSettings.FogColor, t);
        RenderSettings.fogDensity = Mathf.Lerp(lerpStartSettings.FogDensity, lerpEndSettings.FogDensity, t);
    }

    private void RotateSunMoon()
    {
        float nightEnd = morningDuration + dayDuration + eveningDuration + nightDuration;
        float angleX = 360 / nightEnd * timer * -1;
        sunMoonParent.eulerAngles = new Vector3(angleX, 0, 0);
    }

    [Button]
    public void SetTime(TimeOfDay timeOfDay)
    {
        CalculateValues(out float morningEnd, out float dayMidpoint, out float dayEnd, out float eveningEnd, out float nightMidpoint, out float nightEnd);
        if (timeOfDay == TimeOfDay.Morning)
        {
            timer = 0;
        }
        if (timeOfDay == TimeOfDay.Day)
        {
            timer = morningEnd;
        }
        if (timeOfDay == TimeOfDay.Evening)
        {
            timer = dayEnd;
        }
        if (timeOfDay == TimeOfDay.Night)
        {
            timer = eveningEnd;
        }
        PrepareLerp(out float lerpStartValue, out float lerpEndValue, out EnvironmentSettingsSO lerpStartColors, out EnvironmentSettingsSO lerpEndColors);
        ApplyLerp(lerpStartValue, lerpEndValue, lerpStartColors, lerpEndColors);
    }
}
