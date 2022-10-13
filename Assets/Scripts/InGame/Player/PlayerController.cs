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


    void Awake()
    {
        canJump = true;
        jumpSpeed = 0f;
        isJump2 = false;
        isJump1 = false;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && (canJump))
        {
            if (isJump1 || isJump2)
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
            isJump2 = false;
            isJump1 = false;
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
