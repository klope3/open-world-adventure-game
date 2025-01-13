using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//A simple on-screen analog stick for debugging.
public class SimpleAnalog : DirectionalInputProvider, IPointerDownHandler
{
    //private RectTransform rt;
    //[SerializeField] private RectTransform referenceRt;
    //[SerializeField] private float multiplier;
    //[SerializeField] private bool snapBack; //return to neutral position when released
    //private bool active;
    //private Vector2 analogInput;
    //public Vector2 AnalogInput
    //{
    //    get
    //    {
    //        return analogInput;
    //    }
    //}
    //
    //private void Awake()
    //{
    //    rt = GetComponent<RectTransform>();
    //}
    //
    //private void Update()
    //{
    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        active = false;
    //        if (snapBack)
    //        {
    //            rt.anchoredPosition = referenceRt.anchoredPosition;
    //        }
    //    }
    //
    //    Vector2 draggedPosition = rt.anchoredPosition;
    //
    //    if (active)
    //    {
    //        RectTransformUtility.ScreenPointToLocalPointInRectangle(
    //            rt.parent as RectTransform,
    //            Input.mousePosition,
    //            null, // Use the correct camera if the Canvas is in 'Screen Space - Camera' mode
    //            out draggedPosition
    //        );
    //        rt.anchoredPosition = draggedPosition;
    //    }
    //
    //    analogInput = Vector2.ClampMagnitude((draggedPosition - referenceRt.anchoredPosition) * multiplier, 1);
    //}
    //
    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    active = true;
    //}
    //
    //public override Vector2 GetInput()
    //{
    //    return analogInput;
    //}
    public override Vector2 GetInput()
    {
        return Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
}
