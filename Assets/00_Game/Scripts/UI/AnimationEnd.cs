using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEnd : MonoBehaviour {

    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void AnimationFinish()
    {
        anim.SetBool("SplashEnd", true);
    }
        
}
