using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    public float RunSpeed = 3.5f;

    public float MoveSpeed = 2f;

    public float RotationSpeed = 9;

    public bool UseTwoHand = false;

    public bool run;

    public bool canMove;

    public bool Lockon;

    public bool Roll;

    public bool z, x, c, v;

    public bool rolling = false;

    //[HideInInspector]
    public Vector3 moveDir;

    //[HideInInspector]
    public float moveAmount;

    public bool Grounded = false;

    public float toGround = 1f;

    public Transform LookOnTarget;

    private Animator ani;

    [HideInInspector]
    public Rigidbody rig;

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
        ani.SetBool("onGround", Grounded);
        ani.SetBool("lockon", Lockon);
        HandleMoveAnimation(dt);
        HandleTwoHandAnimation();
        HandleRollAnimation(dt);
    }

    public void FixedTick(float dt)
    {
        canMove = ani.GetBool("canMove");
        rolling = ani.GetBool("rolling");

        DetectAction();

        rig.drag = (moveAmount > 0 || Grounded == false) ? 0 : 4;

        if (!canMove || rolling)
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

        Vector3 targetDir = !Lockon ? moveDir : LookOnTarget.position - transform.position;
        targetDir.y = 0;
        if (targetDir == Vector3.zero)
        {
            targetDir = transform.forward;
        }
        targetDir.Normalize();
        Quaternion tr = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, tr, dt * moveAmount * RotationSpeed);

    }

    private void HandleMoveAnimation(float dt)
    {
        if (!Lockon)
        {
            ani.SetFloat("vertical", moveAmount, 0.4f, dt);
            if (moveAmount > 0)
            {
                ani.SetBool("run", run);
            }
        }
        else
        {
            HandleLockMoveAnimation(dt);
        }
    }

    private void HandleLockMoveAnimation(float dt)
    {
        Vector3 relativeDir = transform.InverseTransformDirection(moveDir);
        Debug.Log(relativeDir);
        ani.SetFloat("vertical", relativeDir.z,0.2f,dt);
        ani.SetFloat("horizontal",relativeDir.x, 0.2f, dt);
    }

    private void HandleTwoHandAnimation()
    {
        ani.SetBool("two_handled", UseTwoHand);
    }

    private void HandleRollAnimation(float dt)
    {
        if (rolling) {
            return;
        }

        if (Roll) {
            Debug.Log("roll input");
            rolling = true;
            ani.applyRootMotion = true;
            Vector3 relativeDir = transform.InverseTransformDirection(moveDir);
            ani.CrossFade("Rolls",0.1f);
            if (moveAmount <= 0.3)
            {
                ani.SetFloat("vertical", 0);
                ani.SetFloat("horizontal", 0);
            }
            else {
                ani.SetFloat("vertical", relativeDir.z, 0.2f, dt);
                ani.SetFloat("horizontal", relativeDir.x, 0.2f, dt);
            }
        
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
