using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{


    private PlayerController controller;


    void Start()
    {
        controller = GetComponent<PlayerController>();
    }


    void Update ()
	{

	    float horizontal = Input.GetAxis("Horizontal");
	    float vertical = Input.GetAxis("Vertical");

        controller.Horizontal = horizontal;
        controller.Vertical = vertical;

        controller.Tick(Time.deltaTime);
	}
}
