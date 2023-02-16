using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Mirror;

public class PlayerControl : NetworkBehaviour
{
    GRC inputControl;

    public Transform swivelTarget;

    public float speed;

    public float speedModifier = 12;

    // Start is called before the first frame update
    void Start()
    {
        inputControl.player = this;
    }

    public override void OnStartLocalPlayer()
    {
        //Camera.main.transform.SetParent(transform);
        Camera.main.transform.eulerAngles = new Vector3(20, 0, 0);
        //Camera.main.transform.localPosition = new Vector3(0, 4.35f, -10f);
        Camera.main.GetComponent<CameraFollow>().player = transform;
        Camera.main.GetComponent<CameraFollow>().swivelTarget = swivelTarget;
        inputControl = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<GRC>();

    }


    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            transform.position += (transform.forward * speed * speedModifier) * Time.deltaTime;
            speed = Mathf.Clamp(Vector2.Distance(inputControl.joystick.position, inputControl.joystickOrigin) / 40, 0, 1);

            if (Input.GetKey(KeyCode.W))
            {
                speed = 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                speed = -1;
            }

            Vector2 angle = ((Vector2)inputControl.joystick.position - inputControl.joystickOrigin).normalized;
            float turnAngle = Mathf.Atan2(angle.x, angle.y) * Mathf.Rad2Deg;

            if (speed > 0)
            {
                //transform.eulerAngles = new Vector3(transform.eulerAngles.x, turnAngle, transform.eulerAngles.z);
                //float adjustedTurnAngle = (turnAngle >= 180 ? 360 - turnAngle : turnAngle);
                //float adjustedRotation = (Mathf.Pow(Mathf.Abs(turnAngle), 1.5f) / 3000) * (turnAngle < 0 ? -1 : 1);
                //Debug.Log(adjustedRotation);
                transform.Rotate(Vector3.up, turnAngle / 80);
            }

            //Debug.Log(Vector2.Distance(inputControl.joystick.position, inputControl.joystickOrigin));

            
        }
        GetComponent<Animator>().SetBool("isRunning", speed > 0.1f);

    }

    public void Dive()
    {
        GetComponent<Animator>().SetTrigger("dive");
        Debug.Log(true);
    }

}
