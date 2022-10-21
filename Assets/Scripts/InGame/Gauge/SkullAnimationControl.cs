using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullAnimationControl : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GaugeControl.gaugeCharge == 3)
        {
            anim.SetBool("isDead", true);
        }
        else 
        {
            anim.SetBool("isDead",false);
        }
    }
}
