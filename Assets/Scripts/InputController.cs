using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{


    private PlayerController controller;

    private CameraController cameraController;


    void Start()
    {
        controller = GetComponent<PlayerController>();
        cameraController = GetComponent<CameraController>();
    }


    void Update ()
	{

	    float horizontal = Input.GetAxis("Horizontal");
	    float vertical = Input.GetAxis("Vertical");

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        cameraController.MouseX = mouseX;
        cameraController.MouseY = mouseY;

        controller.moveDir = (cameraController.GetCameraForword() * vertical + cameraController.GetCameraRight() * horizontal).normalized;
        float m = new Vector3(horizontal,vertical).sqrMagnitude;
        controller.moveAmount = Mathf.Clamp01(m);

        controller.Tick(Time.deltaTime);

        cameraController.Tick(Time.deltaTime);


    }
}
