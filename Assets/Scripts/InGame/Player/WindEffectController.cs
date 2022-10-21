using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WindEffectController : MonoBehaviour
{
    public static Animator anim;
    public float position1, position2, position3,position0;
    public GameObject player;
    private int position;

    void Start()
    {
        anim = GetComponent<Animator>();
        position = 0;
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
        switch (position)
        {
            case 0:
                {
                    this.gameObject.transform.position = new Vector3(this.transform.position.x, player.transform.position.y + position0, this.transform.position.z);
                    break;
                }
            case 1:
                {
                    this.gameObject.transform.position = new Vector3(this.transform.position.x, player.transform.position.y + position1, this.transform.position.z);
                    break;
                }
            case 2:
                {
                    this.gameObject.transform.position = new Vector3(this.transform.position.x, player.transform.position.y + position2, this.transform.position.z);
                    break;
                }
            case 3:
                {
                    this.gameObject.transform.position = new Vector3(this.transform.position.x, player.transform.position.y + position3, this.transform.position.z);
                    break;
                }
        }
    }

    void SetPosition0()
    {
        position = 0;
    }

    void SetPosition1()
    {
        position = 1;
    }

    void SetPosition2()
    {
        position = 2;
    }

    void SetPostion3()
    {
        position = 3;
    }

    void Disappear()
    {
        anim.SetBool("CanDisappear", false);
    }
}
