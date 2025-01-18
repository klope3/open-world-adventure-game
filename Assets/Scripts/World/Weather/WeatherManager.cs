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

    [SerializeField] private float timeMultiplier = 0.00278f;
    [ShowInInspector, DisplayAsString] private float timer;
    [ShowInInspector, DisplayAsString] public TimeOfDay CurrentTimeOfDay
    {
        get
        {
            if (timer >= MORNING_START && timer < DAY_START) return TimeOfDay.Morning;
            if (timer >= DAY_START && timer < EVENING_START) return TimeOfDay.Day;
            if (timer >= EVENING_START && timer < NIGHT_START) return TimeOfDay.Evening;
            return TimeOfDay.Night;
        }
    }

    private readonly float MORNING_START = 0;
    private readonly float DAY_START = 0.0625f;
    private readonly float EVENING_START = 0.4375f;
    private readonly float NIGHT_START = 0.5f;

    private readonly float FULL_NIGHT_COLORS_START = 0.625f; //environment will be 100% night-colored at this time
    private readonly float FULL_NIGHT_COLORS_END = 0.875f; //environment will finish being 100% night-colored at this time; during the night the colors remain consistent; start lerping toward day colors

    public enum TimeOfDay
    {
        Morning,
        Day,
        Evening,
        Night
    }

    private void Update()
    {
        timer += Time.deltaTime * timeMultiplier;
        if (timer > 1) timer = 0;

        PrepareLerp(out float lerpStartValue, out float lerpEndValue, out EnvironmentSettingsSO lerpStartColors, out EnvironmentSettingsSO lerpEndColors);
        ApplyLerp(lerpStartValue, lerpEndValue, lerpStartColors, lerpEndColors);
        RotateSunMoon();
    }

    private void PrepareLerp(out float lerpStartValue, out float lerpEndValue, out EnvironmentSettingsSO lerpStartColors, out EnvironmentSettingsSO lerpEndColors)
    {
        if (CurrentTimeOfDay == TimeOfDay.Morning)
        {
            lerpStartValue = MORNING_START;
            lerpEndValue = DAY_START;
            lerpStartColors = morningColors;
            lerpEndColors = dayColors;
        } else if (CurrentTimeOfDay == TimeOfDay.Day)
        {
            lerpStartValue = 1;
            lerpEndValue = 1;
            lerpStartColors = dayColors;
            lerpEndColors = dayColors; //essentially no color change during day
        } else if (CurrentTimeOfDay == TimeOfDay.Evening)
        {
            lerpStartValue = EVENING_START;
            lerpEndValue = NIGHT_START;
            lerpStartColors = dayColors;
            lerpEndColors = eveningColors;
        } else if (timer >= NIGHT_START && timer < FULL_NIGHT_COLORS_START)
        {
            lerpStartValue = NIGHT_START;
            lerpEndValue = FULL_NIGHT_COLORS_START;
            lerpStartColors = eveningColors;
            lerpEndColors = nightColors;
        } else if (timer >= FULL_NIGHT_COLORS_START && timer < FULL_NIGHT_COLORS_END)
        {
            lerpStartValue = 1;
            lerpEndValue = 1;
            lerpStartColors = nightColors;
            lerpEndColors = nightColors; //essentially no color change during night
        } else
        {
            lerpStartValue = FULL_NIGHT_COLORS_END;
            lerpEndValue = 1;
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
        sunMoonParent.eulerAngles = new Vector3(timer * -360, 0, 0);
    }

    [Button]
    public void SetTime(TimeOfDay timeOfDay)
    {
        if (timeOfDay == TimeOfDay.Morning)
        {
            timer = MORNING_START;
        }
        if (timeOfDay == TimeOfDay.Day)
        {
            timer = DAY_START;
        }
        if (timeOfDay == TimeOfDay.Evening)
        {
            timer = EVENING_START;
        }
        if (timeOfDay == TimeOfDay.Night)
        {
            timer = NIGHT_START;
        }
        PrepareLerp(out float lerpStartValue, out float lerpEndValue, out EnvironmentSettingsSO lerpStartColors, out EnvironmentSettingsSO lerpEndColors);
        ApplyLerp(lerpStartValue, lerpEndValue, lerpStartColors, lerpEndColors);
        RotateSunMoon();
    }
}
