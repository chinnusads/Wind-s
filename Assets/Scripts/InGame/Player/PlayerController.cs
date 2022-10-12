using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class PlayerController : MonoBehaviour
{
    //jump
    public float jumpStartSpeed1, jumpStartSpeed2;
    public float gravity1, gravity2;
    public float jumpTime1,jumpTime2;//uptime only
    [SerializeField]private float jumpSpeed;
    [SerializeField]private float nowJumpTime;
    [SerializeField]private bool canJump;
    private int gauge;
    

    void Awake()
    {
        canJump = true;
        jumpSpeed = 0f;
    }

    void Update()
    {
        gauge = GaugeControl.gaugeCharge;
        {
            if ((Input.GetKeyDown(KeyCode.Space)) && (canJump))
            {
                if ((gauge == 1) || (gauge == 2))
                {
                    canJump = false;
                    nowJumpTime = 0f;
                }
            }
            if (!canJump)
            {
                Jump();
            }
            if (this.gameObject.transform.position.y <= 0f)
            {
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 0f, this.gameObject.transform.position.z);
                canJump = true;
                nowJumpTime = 0f;
                jumpSpeed = 0f;
            }


        }
    }

    void Jump()
    {
        if (gauge == 1)
        {
            if (nowJumpTime >= jumpTime1)
            {
                JumpDown(gravity1);
            }
            else
            {
                JumpUp(jumpStartSpeed1);
            }
        }
        else if (gauge == 2)
        {
            if (nowJumpTime < jumpTime2)
            {
                JumpUp(jumpStartSpeed2);
            }
            else
            {
                JumpDown(gravity2);
            }
        }
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y +  jumpSpeed * Time.deltaTime, this.gameObject.transform.position.z);
        nowJumpTime += Time.deltaTime;
    }

    void JumpUp(float jumpStartSpeed)
    {
        jumpSpeed = jumpStartSpeed;
    }

    void JumpDown(float gravity)
    {
        if (jumpSpeed > 0f)
        {
            jumpSpeed = 0f;
        }
        jumpSpeed -= gravity * Time.deltaTime;
    }

}
