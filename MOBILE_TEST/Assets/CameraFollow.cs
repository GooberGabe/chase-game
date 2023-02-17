using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public Transform swivelTarget;

    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            Quaternion eulerCoversion = Quaternion.Euler(new Vector3(transform.eulerAngles.x, player.eulerAngles.y, transform.eulerAngles.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, eulerCoversion, 0.1f);
            transform.position = Vector3.Lerp(transform.position, swivelTarget.position, 0.1f);
        }

    }

}
