using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    public float RunSpeed = 3.5f;

    public float MoveSpeed = 2f;

    public float RotationSpeed = 9;

    //[HideInInspector]
    public Vector3 moveDir;

    //[HideInInspector]
    public float moveAmount;

    public bool Grounded = false;

    public float toGround = 1f;

    public bool run;

    private Animator ani;

    private Rigidbody rig;

    void Start()
    {
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        rig.angularDrag = 999;
        rig.drag = 4;
    }

    public void Tick(float dt)
    {
        OnGround();

        ani.SetFloat("vertical",moveAmount,0.4f,dt);

        rig.drag = (moveAmount > 0) ? 0 : 4;


        float targetSpeed = MoveSpeed;
        if (run)
        {
            targetSpeed = RunSpeed;
        }

        if (Grounded)
        {
            rig.velocity = moveDir * targetSpeed * moveAmount;
        }
        

        Quaternion tr = Quaternion.LookRotation(moveDir);
        transform.rotation = Quaternion.Slerp(transform.rotation,tr,dt * moveAmount * RotationSpeed);
    }

    public bool OnGround()
    {

        Grounded = false;

        bool r = false;

        Vector3 origin = transform.position + Vector3.up * toGround;
        Vector3 dir = -Vector3.up;
        float distance = toGround - 0.3f;

        RaycastHit hit;
        Ray ray = new Ray();
        ray.origin = origin;
        ray.direction = dir;

        Debug.DrawRay(origin,dir * distance);
        if (Physics.Raycast(origin,dir,out hit,distance,LayerMask.GetMask(new string[] { "Ground" })))
        {
            Grounded = true;
            Vector3 targetPos = hit.point;
            targetPos.y += toGround;
            transform.position = targetPos;
        }

        return r;
    }

}
