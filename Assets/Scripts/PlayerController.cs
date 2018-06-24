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

    public bool usingItem;

    public bool Lockon;

    public bool Roll;

    public bool UseItem;

    public bool lt, lb, rt, rb;

    public bool rolling = false;

    //[HideInInspector]
    public Vector3 moveDir;

    //[HideInInspector]
    public float moveAmount;

    public bool Grounded = false;

    public float toGround = 1f;

    public Transform LookOnTarget;

    private Animator ani;

    private ActionManager actionManager;

    private InventoryManager inventoryManager;

    [HideInInspector]
    public Rigidbody rig;

    void Start()
    {
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        actionManager = GetComponent<ActionManager>();
        inventoryManager = GetComponent<InventoryManager>();

        inventoryManager.Init(this, actionManager);
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
        HandleUseItemAnimation();
    }

    public void FixedTick(float dt)
    {
        canMove = ani.GetBool("canMove");
        rolling = ani.GetBool("rolling");
        usingItem = ani.GetBool("usingItem");

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

    private void HandleUseItemAnimation() {
        if (!canMove || rolling || usingItem) {
            return;
        }


        if (UseItem) {
            Debug.Log("using item");
            ani.CrossFade("UseItem",0.1f);
        }
    }

    private void DetectAction()
    {
        if (lt == false && lb == false && rt == false && rb == false)
        {
            return ;
        }

        if (!canMove || usingItem || rolling)
        {
            return;
        }

        ActionManager.ActionSlot slot = null;

  
        if (lt)
        {
            slot = actionManager.GetActionSlot(ActionManager.ActionSlotType.LT);
        }
        if (lb)
        {
            slot = actionManager.GetActionSlot(ActionManager.ActionSlotType.LB);
        }
        if (rt)
        {
            slot = actionManager.GetActionSlot(ActionManager.ActionSlotType.RT);
        }
        if (rb)
        {
            slot = actionManager.GetActionSlot(ActionManager.ActionSlotType.RB);
        }

        if (slot == null) {
            return;
        }

        string targetAnimation = slot.AnimationName;
        Debug.Log(targetAnimation);
        if (!string.IsNullOrEmpty(targetAnimation))
        {
            ani.SetBool("mirror",slot.Mirror);
            ani.CrossFade(targetAnimation,0.3f);
           
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
