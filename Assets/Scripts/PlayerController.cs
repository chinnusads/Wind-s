using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //jump
    public float JumpStartSpeed;
    public float gravity;
    private float JumpSpeed;
    private float jumpTime;
    private bool canJump;
    //

    void Awake()
    {
        canJump = true;
        JumpSpeed = 0f;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && (canJump))
        {
            canJump = false;
            JumpSpeed = JumpStartSpeed;
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
