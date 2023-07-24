using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingMenu: MonoBehaviour
{
    private Animator slideAnimator;
    private static SlidingMenu instance;
    public static SlidingMenu Instance => instance;
    private void Awake()
    {
        slideAnimator = GetComponent<Animator>();
        instance = this;
    }
    public void SlideLeft()
    {
        slideAnimator.SetTrigger("Left");
    }
    public void SlideRight()
    {
        slideAnimator.SetTrigger("Right");
    }
}
