using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Mirror;

public class PlayerControl : NetworkBehaviour
{
    GRC inputControl;
    Rigidbody rb;
    public Transform swivelTarget;

    public float speed;
    public float speedModifier = 12;
    //public float gravity = 1;

    public bool shouldRun = false;

    // Start is called before the first frame update
    void Start()
    {
        inputControl.player = this;
        rb = GetComponent<Rigidbody>();
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
            speed = Mathf.Clamp(Vector2.Distance(inputControl.joystick.position, inputControl.joystickOrigin) / 50, 0, 1);

            if (Input.GetKey(KeyCode.W))
            {
                speed = 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                speed = -1;
            }

            if (shouldRun)
            {
                transform.position += ((transform.forward * speed * speedModifier) * Time.deltaTime);
            }

            

            Vector2 angle = ((Vector2)inputControl.joystick.position - inputControl.joystickOrigin).normalized;
            float turnAngle = Mathf.Atan2(angle.x, angle.y) * Mathf.Rad2Deg;

            if (speed > 0)
            {
                //transform.eulerAngles = new Vector3(transform.eulerAngles.x, turnAngle, transform.eulerAngles.z);
                //float adjustedTurnAngle = (turnAngle >= 180 ? 360 - turnAngle : turnAngle);
                //float adjustedRotation = (Mathf.Pow(Mathf.Abs(turnAngle), 1.5f) / 3000) * (turnAngle < 0 ? -1 : 1);
                //Debug.Log(adjustedRotation);
                transform.Rotate(Vector3.up, ((2 * Mathf.Pow(turnAngle,3)) /  (Mathf.Pow(turnAngle, 2) + 9000)) / 80);
            }

            //Debug.Log(Vector2.Distance(inputControl.joystick.position, inputControl.joystickOrigin));

            //characterController.SimpleMove(Vector3.down * gravity * Time.deltaTime);
            
        }
        GetComponent<Animator>().SetBool("isRunning", speed > 0.1f);

    }

    public void DiveForce()
    {
        rb.AddForce(transform.forward * 1600);
        Debug.Log("HI");
    }
    public void DiveTrigger()
    {
        if (shouldRun)
        {
            GetComponent<Animator>().SetTrigger("dive");
        }
    }

}
