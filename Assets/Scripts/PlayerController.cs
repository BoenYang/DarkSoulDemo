using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    public float RunSpeed = 3.5f;

    public float MoveSpeed = 2f;

    public float RotationSpeed = 9;

    public bool UseTwoHanld = false;

    public bool run;

    public bool z, x, c, v;

    //[HideInInspector]
    public Vector3 moveDir;

    //[HideInInspector]
    public float moveAmount;

    public bool Grounded = false;

    public float toGround = 1f;

    private Animator ani;

    private Rigidbody rig;

    public bool canMove;

    void Start()
    {
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        rig.angularDrag = 999;
        rig.drag = 4;
    }

    public void Tick(float dt)
    {
        Grounded = OnGround();
        HandleAnimation(dt);
    }

    public void FixedTick(float dt)
    {
        canMove = ani.GetBool("canMove");

        DetectAction();

        rig.drag = (moveAmount > 0 || Grounded == false) ? 0 : 4;

        if (!canMove)
        {
            return;
        }

        float targetSpeed = MoveSpeed;
        if (run)
        {
            targetSpeed = RunSpeed;
        }

        if (Grounded)
        {
            rig.velocity = moveDir * targetSpeed * moveAmount;
        }

        Vector3 targetDir = moveDir;
        targetDir.y = 0;
        if (targetDir == Vector3.zero)
        {
            targetDir = transform.forward;
        }

        Quaternion tr = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, tr, dt * moveAmount * RotationSpeed);
    }

    private void HandleAnimation(float dt)
    {
        ani.SetBool("onGround",Grounded);
        ani.SetFloat("vertical", moveAmount, 0.4f, dt);
        ani.SetBool("two_handled", UseTwoHanld);
        if (moveAmount > 0)
        {
            ani.SetBool("run", run);
        }
    }

    private void DetectAction()
    {
        if (z == false && x == false && c == false && v == false)
        {
            return ;
        }

        if (!canMove)
        {
            return;
        }

        string targetAnimation = null;
        if (z)
        {
            targetAnimation = "oh_attack_1";
        }
        if (x)
        {
            targetAnimation = "oh_attack_2";
        }
        if (c)
        {
            targetAnimation = "oh_attack_3";
        }
        if (v)
        {
            targetAnimation = "th_attack_1";
        }

        if (!string.IsNullOrEmpty(targetAnimation))
        {
            ani.CrossFade(targetAnimation,0.1f);
        }
    }

    public bool OnGround()
    {
        bool r = false;

        Vector3 origin = transform.position + Vector3.up * toGround;
        Vector3 dir = - Vector3.up;
        float distance = toGround + 0.3f;

        RaycastHit hit;

        Debug.DrawRay(origin,dir * distance);
        if (Physics.Raycast(origin,dir,out hit,distance,~LayerMask.NameToLayer("Ground")))
        {
            r = true;
            Vector3 targetPos = hit.point;
            transform.position = targetPos;
        }
        return r;
    }

}
