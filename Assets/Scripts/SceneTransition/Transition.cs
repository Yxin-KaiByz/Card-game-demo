using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTrans()
    {
        animator.SetTrigger("Start");
    }

    public void EndTrans()
    {
        animator.SetTrigger("End");
    }

    public bool isAnimationDone()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime>=1)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
