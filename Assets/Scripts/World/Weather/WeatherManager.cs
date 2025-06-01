using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private GameClock gameClock;

    private readonly float FULL_NIGHT_COLORS_START = 0.625f; //environment will be 100% night-colored at this time
    private readonly float FULL_NIGHT_COLORS_END = 0.875f; //environment will finish being 100% night-colored at this time; during the night the colors remain consistent; start lerping toward day colors

    private void Start()
    {
        gameClock = GameObject.FindGameObjectWithTag("GameClock").GetComponent<GameClock>();
    }

    private void Update()
    {
        PrepareLerp(out float lerpStartValue, out float lerpEndValue, out EnvironmentSettingsSO lerpStartColors, out EnvironmentSettingsSO lerpEndColors);
        ApplyLerp(lerpStartValue, lerpEndValue, lerpStartColors, lerpEndColors);
        RotateSunMoon();
    }

    private void PrepareLerp(out float lerpStartValue, out float lerpEndValue, out EnvironmentSettingsSO lerpStartColors, out EnvironmentSettingsSO lerpEndColors)
    {
        if (gameClock.CurrentTimeOfDay == GameClock.TimeOfDay.Morning)
        {
            lerpStartValue = GameClock.MORNING_START;
            lerpEndValue = GameClock.DAY_START;
            lerpStartColors = morningColors;
            lerpEndColors = dayColors;
        } else if (gameClock.CurrentTimeOfDay == GameClock.TimeOfDay.Day)
        {
            lerpStartValue = 1;
            lerpEndValue = 1;
            lerpStartColors = dayColors;
            lerpEndColors = dayColors; //essentially no color change during day
        } else if (gameClock.CurrentTimeOfDay == GameClock.TimeOfDay.Evening)
        {
            lerpStartValue = GameClock.EVENING_START;
            lerpEndValue = GameClock.NIGHT_START;
            lerpStartColors = dayColors;
            lerpEndColors = eveningColors;
        } else if (gameClock.DayTimer >= GameClock.NIGHT_START && gameClock.DayTimer < FULL_NIGHT_COLORS_START)
        {
            lerpStartValue = GameClock.NIGHT_START;
            lerpEndValue = FULL_NIGHT_COLORS_START;
            lerpStartColors = eveningColors;
            lerpEndColors = nightColors;
        } else if (gameClock.DayTimer >= FULL_NIGHT_COLORS_START && gameClock.DayTimer < FULL_NIGHT_COLORS_END)
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
        float t = (gameClock.DayTimer - lerpStartValue) / (lerpEndValue - lerpStartValue);
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
        sunMoonParent.eulerAngles = new Vector3(gameClock.DayTimer * -360, 0, 0);
    }
}
