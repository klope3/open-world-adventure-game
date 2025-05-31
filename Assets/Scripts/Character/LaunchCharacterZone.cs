using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ECM2;

public class LaunchCharacterZone : MonoBehaviour
{
    [SerializeField] private GameObjectDetectorZone detectorZone;
    [SerializeField] private float strength;
    public UnityEvent OnLaunch;

    public void Launch()
    {
        List<GameObject> objects = detectorZone.GetObjectsList();
        foreach (GameObject obj in objects)
        {
            Character character = obj.GetComponent<Character>();
            if (character == null) continue;

            character.PauseGroundConstraint();
            character.LaunchCharacter(transform.forward * strength, true);
        }

        OnLaunch?.Invoke();
    }
}
