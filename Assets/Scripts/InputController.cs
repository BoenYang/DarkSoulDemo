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

	    controller.run = Input.GetKey(KeyCode.LeftShift);

        cameraController.MouseX = mouseX;
        cameraController.MouseY = mouseY;

        controller.moveDir = (cameraController.GetCameraForword() * vertical + cameraController.GetCameraRight() * horizontal).normalized;
        float m = new Vector3(horizontal,vertical).sqrMagnitude;
        controller.moveAmount = Mathf.Clamp01(m);

        controller.Tick(Time.deltaTime);

        cameraController.Tick(Time.deltaTime);

        //lt
	    controller.lt = Input.GetKey(KeyCode.Z);
        //lb
	    controller.lb = Input.GetKey(KeyCode.X);
        //rt
	    controller.rt = Input.GetKey(KeyCode.C);
        //rb
	    controller.rb = Input.GetKey(KeyCode.V);

        controller.UseItem = Input.GetKey(KeyCode.Alpha1);


	    bool a = Input.GetKeyDown(KeyCode.Q);
	    if (a)
	    {
	        controller.Lockon = !controller.Lockon;
	        if (controller.LookOnTarget == null)
	        {
	            controller.Lockon = false;
	        }

	        cameraController.LockTarget = controller.LookOnTarget;
	        cameraController.lockon = controller.Lockon;
	    }

	    bool e = Input.GetKeyDown(KeyCode.E);
        controller.Roll = e;
	}

    void FixedUpdate()
    {
        controller.FixedTick(Time.fixedDeltaTime);
    }
}
