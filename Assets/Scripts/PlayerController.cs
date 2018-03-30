using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float Horizontal;

    public float Vertical;

    private Animator ani;

    void Start()
    {
        ani = GetComponent<Animator>();
    }

    public void Tick(float dt)
    {
        ani.SetFloat("vertical",Vertical);
    }

}
