using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniamtionHook : MonoBehaviour
{
    private PlayerController controller;

    private Animator ani;

    public void Init(PlayerController c,Animator animator)
    {
        controller = c;
        ani = animator;
    }

    private void OnAnimatorMove()
    {
        if (controller.canMove)
        {
            return ;
        }

    }

}
