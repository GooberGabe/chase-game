using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class GRC : MonoBehaviour
{
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    public RectTransform joystick;

    // The location the joystick started in. 
    public Vector2 joystickOrigin;

    void Start()
    {
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();

        joystickOrigin = joystick.position;
    }

    void Update()
    {
        bool jsTouch = false;
        if (Input.touchCount > 0)
        {
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            m_Raycaster.Raycast(m_PointerEventData, results);

            
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.name == "Joystick")
                {
                    joystick.position = Input.mousePosition;
                    jsTouch = true;

                }
            }
        }
        if (!jsTouch)
        {
            joystick.position = Vector2.Lerp(joystick.position, joystickOrigin, 0.1f);
            

        }
        if (Vector2.Distance(joystick.position, joystickOrigin) < 0.1f)
        {
            joystick.position = joystickOrigin;
        }
    }
}