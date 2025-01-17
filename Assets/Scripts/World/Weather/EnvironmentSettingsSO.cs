using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnvironmentColorsSO", menuName = "Scriptable Objects/EnvironmentColorsSO")]
public class EnvironmentSettingsSO : ScriptableObject
{
    [SerializeField] private Color horizonColor;
    [SerializeField] private Color zenithColor;
    [SerializeField] private Color ambientLightColor;
    [SerializeField] private Color sunColor;
    [SerializeField] private Color sunInnerColor;
    [SerializeField] private Color sunOuterColor;
    [SerializeField] private Color cloudColor;
    [SerializeField] private Color fogColor;
    [SerializeField] private float fogDensity;

    public Color HorizonColor
    {
        get
        {
            return horizonColor;
        }
    }
    public Color ZenithColor
    {
        get
        {
            return zenithColor;
        }
    }
    public Color AmbientLightColor
    {
        get
        {
            return ambientLightColor;
        }
    }
    public Color SunColor
    {
        get
        {
            return sunColor;
        }
    }
    public Color SunInnerColor
    {
        get
        {
            return sunInnerColor;
        }
    }
    public Color SunOuterColor
    {
        get
        {
            return sunOuterColor;
        }
    }
    public Color CloudColor
    {
        get
        {
            return cloudColor;
        }
    }
    public Color FogColor
    {
        get
        {
            return fogColor;
        }
    }
    public float FogDensity
    {
        get
        {
            return fogDensity;
        }
    }
}
