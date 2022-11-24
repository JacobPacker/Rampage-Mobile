using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler
{
    private RectTransform joystickTransform;

    [SerializeField]
    private float dragThreshold = 0.6f;
    [SerializeField]
    private float dragMoveDis = 30;
    [SerializeField]
    private float dragOffsetDis = 100;

    public event Action<Vector2> OnMove;

    [HideInInspector] public float x;
    [HideInInspector] public float y;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 offset;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickTransform,
            eventData.position,
            null,
            out offset);
        offset = Vector2.ClampMagnitude(offset, dragOffsetDis) / dragMoveDis;

        joystickTransform.anchoredPosition = offset * dragMoveDis;

        Vector2 inputVector = CalculateMovementInput(offset);
        OnMove?.Invoke(inputVector);

        //Debug.Log(offset);
    }

    private Vector2 CalculateMovementInput(Vector2 offset)
    {
        x = Mathf.Abs(offset.x) > dragThreshold ? offset.x : 0;
        y = Mathf.Abs(offset.y) > dragThreshold ? offset.y : 0;
        return new Vector2(x, y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystickTransform.anchoredPosition = Vector2.zero;
        OnMove?.Invoke(Vector2.zero);
        x = 0;
        y = 0;
    }

    private void Awake()
    {
        joystickTransform = (RectTransform)transform;
    }
}
