using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    public float RunSpeed = 3.5f;

    public float MoveSpeed = 2f;

    [HideInInspector]
    public Vector3 moveDir;

    [HideInInspector]
    public float moveAmount;

    private Animator ani;

    private Rigidbody rig;

    void Start()
    {
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
    }

    public void Tick(float dt)
    {
        ani.SetFloat("vertical",moveAmount);


        rig.velocity = moveDir * MoveSpeed;

    }

}
