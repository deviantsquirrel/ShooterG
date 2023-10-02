using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJoystick : MonoBehaviour
{
    public GameObject joystick;
    public GameObject joystickBG;
    public Vector2 joystickVec;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;

    void Start()
    {
        joystickOriginalPos = joystickBG.transform.position;
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 4;
    }

    public void PointerDown()
    {
        joystick.transform.position = Input.mousePosition;
        joystickBG.transform.position = Input.mousePosition;
    }

    public void Drag(BaseEventData baseEventData)
    {

        
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVec = (dragPos - joystickOriginalPos).normalized;

        float joystickDist = Vector2.Distance(dragPos, joystickOriginalPos);

        if (joystickDist < joystickRadius)
        {
            joystickBG.transform.position = joystickOriginalPos + joystickVec * joystickDist;
            joystick.transform.position = joystickOriginalPos + joystickVec * joystickDist;

        }
        else
        {
            joystickBG.transform.position = joystickOriginalPos + joystickVec * joystickRadius;
            joystick.transform.position = joystickOriginalPos + joystickVec * joystickRadius;
        }
    }

    public void PointerUp()
    {
        joystickVec = Vector2.zero;
        joystick.transform.position = joystickOriginalPos;
        joystickBG.transform.position = joystickOriginalPos;
    }
}
