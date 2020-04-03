using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public bool elevationInLocalSpace = false;
    public float speed = 1f;
    public float speedClamp = 15f;
    public float sensitivity = 1f;

    public KeyCode toggleElevationSpace = KeyCode.X;

    bool released = true;

    private void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 elev = new Vector3(0, Input.GetAxis("Elevate"), 0);
        Vector3 look = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        speed += Mathf.Clamp(Input.GetAxis("Mouse ScrollWheel"), -10f, 10f);
        speed = Mathf.Clamp(speed, 0.01f, speedClamp);

        transform.Translate(move * speed);
        transform.Translate(elev * speed, (elevationInLocalSpace) ? Space.Self : Space.World);

        transform.Rotate(look * sensitivity);
        Vector3 clampZ = transform.rotation.eulerAngles;
        clampZ.z = 0;
        transform.rotation = Quaternion.Euler(clampZ);

        if (Input.GetKeyDown(toggleElevationSpace) && released) elevationInLocalSpace = !elevationInLocalSpace;
        if (Input.GetKeyUp(toggleElevationSpace)) released = true;
    }
}
