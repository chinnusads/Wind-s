using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //jump
    public float JumpStartSpeed;
    public float gravity;
    private float JumpSpeed;
    private float jumpTime;
    public static bool canJump;

    public float Level2JumpSpeed;//2段階ジャンプのスピード
    public float Level2Gravity;//2段階ジャンプの重力
    //
    public static bool isJump1, isJump2;

    void Awake()
    {
        canJump = true;
        JumpSpeed = 0f;
        isJump1 = false;
        isJump2 = false;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && (canJump))
        {
            if (isJump1)
            {
                Debug.Log(("jump1"));
                canJump = false;
                JumpSpeed = JumpStartSpeed; //1段ジャンプ
                isJump1 = false;
            }
            else if (isJump2)
            {
                Debug.Log(("jump2"));
                canJump = false;
                JumpSpeed = Level2JumpSpeed;//2段ジャンプ
                isJump2 = false;
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
        }    
        
        
    }

    //重力のある状態をマネする
    void Jump()
    {
        SpeedUpdate();
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y +  JumpSpeed * Time.deltaTime, this.gameObject.transform.position.z);
    }

    void SpeedUpdate()
    {
        JumpSpeed = JumpSpeed  - gravity * Time.deltaTime;
    }

    
}
