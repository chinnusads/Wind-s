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
    private float jumpSpeed;
    private float nowJumpTime;
    public static bool canJump;
    public static bool isJump1, isJump2;
    private bool firstTimeJump;//1回目のジャンプ
    

    void Awake()
    {
        canJump = true;
        jumpSpeed = 0f;
        isJump2 = false;
        isJump1 = false;
        firstTimeJump = true;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space))&& (canJump))
        {
            if(firstTimeJump)//これは一回目のジャンプとしたら
            {
                if (isJump1 || isJump2)
                {
                    Debug.Log(("isJump1-"));
                    //canJump = false;
                    nowJumpTime = 0f;
                }
                firstTimeJump = false;
            }
            else if (!firstTimeJump)//もう二回目のジャンプとしたら
            {
                Debug.Log(("isJump2-1"));
                canJump = false;
                nowJumpTime = 0f;
                
            }
        }
        if ((!canJump)||(!firstTimeJump))
        {
            Jump();
        }
        if (this.gameObject.transform.position.y <= 0f)//着地
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 0f, this.gameObject.transform.position.z);
            canJump = true;
            nowJumpTime = 0f;
            jumpSpeed = 0f;
            isJump2 = false;
            isJump1 = false;
            firstTimeJump = true;
        }
    }

    void Jump()
    {
        if (isJump1)
        {
            Debug.Log("jump1-1");
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
            Debug.Log("jump1-2");
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
