using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    GameObject uni;

    Vector3 defaultPos;
    Quaternion defaultRot;
    float defaultFOV;

    private void Start()
    {
        uni = GameObject.Find("CameraParent");
        defaultPos = gameObject.transform.position;
        defaultRot = uni.transform.rotation;
        defaultFOV = 30;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Camera.main.transform.Translate(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"), 0);
        }

        if (Input.GetMouseButton(1))
        {
            uni.transform.Rotate(Input.GetAxisRaw("Mouse Y") * 10f, Input.GetAxisRaw("Mouse X") * 10f, 0);
        }

        Camera.main.fieldOfView -= 20 * Input.GetAxis("Mouse ScrollWheel");
        if (Camera.main.fieldOfView < 10)
        {
            Camera.main.fieldOfView = 10;
        }

        if (Input.GetMouseButtonDown(2))
        {
            Camera.main.transform.position = defaultPos;
            uni.transform.rotation = defaultRot;
            Camera.main.fieldOfView = defaultFOV;
        }
    }
}
