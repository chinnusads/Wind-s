using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class PlayerController : MonoBehaviour
{
    //jump
    public float jumpStartSpeed1, jumpStartSpeed2;
    public float gravity1, gravity2;
    public float jumpTime1, jumpTime2;//uptime only
    private float jumpSpeed;
    private float nowJumpTime;
    public static bool canJump;
    public static bool isJump1, isJump2;
    public static int jumpState;// 0:on the ground; 1:first time jump; 2:second time jump;


    void Awake()
    {
        canJump = true;
        jumpSpeed = 0f;
        isJump2 = false;
        isJump1 = false;
        jumpState = 0;
    }

    void Update()
    {
        JumpInput();
        if (jumpState > 0)
        {
            Jump();
        }
        if (this.gameObject.transform.position.y <= 0f)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 0f, this.gameObject.transform.position.z);
            jumpState = 0;
            nowJumpTime = 0f;
            jumpSpeed = 0f;
            isJump2 = false;
            isJump1 = false;
        }
    }

    void JumpInput()
    {
        if (jumpState == 0)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || (JoyconInput.jump)) && (GaugeControl.gaugeCharge > 0) && (GaugeControl.gaugeCharge < 3))
            {
                jumpState = 1;
                if (GaugeControl.gaugeCharge == 1)
                {
                    isJump1 = true;
                    isJump2 = false;
                    GaugeControl.gaugeCount -= 0.2f;
                }
                if (GaugeControl.gaugeCharge == 2)
                {
                    isJump2 = true;
                    isJump1 = false;
                    GaugeControl.gaugeCount -= 0.6f;
                }
                nowJumpTime = 0f;

            }
        }
        else if (jumpState == 1)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || (JoyconInput.jump)) && (GaugeControl.gaugeCharge == 1))
            {
                jumpState = 2;
                isJump1 = true;
                isJump2 = false;
                nowJumpTime = 0f;
                GaugeControl.gaugeCount -= 0.2f;
            }
        }
        else if (jumpState == 2)
        {

        }
    }

    void Jump()
    {
        if (isJump1)
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
        else if (isJump2)
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
