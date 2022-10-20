using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WindEffectController : MonoBehaviour
{
    public static Animator anim;
    private bool animDisappear;

    void Start()
    {
        anim = GetComponent<Animator>();
        animDisappear = false;
    }


    void Update()
    {
        anim.SetInteger("GaugeCharge", GaugeControl.gaugeCharge);
        anim.SetFloat("GaugeCount", GaugeControl.gaugeCount);
        anim.SetFloat("JumpSpeed", PlayerController.jumpSpeed);
        anim.SetBool("isJump1", PlayerController.isJump1);
        anim.SetBool("isJump2", PlayerController.isJump2);
        if (PlayerController.jumpSpeed >= 0)
        {
            anim.SetBool("CanDisappear", true); 
        }
        if (GaugeControl.gaugeCount == 0f)
        {
            anim.SetBool("NoCharge",true);
        }
        else
        {
            anim.SetBool("NoCharge", false);
        }
    }

    void Disappear()
    {
        anim.SetBool("CanDisappear", false);
    }
}
