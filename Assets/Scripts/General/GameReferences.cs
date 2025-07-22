using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameReferences
{
    private static GameObject _player;
    public static GameObject Player
    {
        get
        {
            if (_player == null) _player = GameObject.FindGameObjectWithTag("Player");
            if (_player == null) Debug.LogError("Player not found!");
            return _player;
        }
    }
}
