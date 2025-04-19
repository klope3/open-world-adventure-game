using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTitleCard : MonoBehaviour
{
    [SerializeField] private string text;

    private void Awake()
    {
        GameObject textObj = GameObject.FindGameObjectWithTag("SceneTitleCardText");
        if (textObj == null) return;

        SceneTitleCardText text = textObj.GetComponent<SceneTitleCardText>();
        text.FadeInText(this.text);
    }
}
