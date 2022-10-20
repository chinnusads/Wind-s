using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffectController : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        anim.SetFloat("GaugeCount", GaugeControl.gaugeCount); 
    }
}
