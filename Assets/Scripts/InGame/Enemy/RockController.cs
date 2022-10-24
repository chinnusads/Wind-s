using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    private GameObject player;
    Vector3 direction;
    public float startSpeed;
    public float accel;
    public float lengthX, lengthY;
    private float moveSpeed;
    private float accelTime;
    public float rotateSpeed;
    private Vector3 pos;

    void Start()
    {
        player = GameObject.Find("PlayerColliderShort");
        if (this.gameObject.transform.position.x > 0)
        {
            pos = new Vector3(this.gameObject.transform.position.x * -1, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        }
        else
        {
            pos = this.gameObject.transform.position;
        }
        direction = player.transform.position - pos;
        direction = direction.normalized;
        moveSpeed = startSpeed;
        accelTime = 0f;
    }

    void FixedUpdate()
    {
        if (accelTime > 1f)
        {
            moveSpeed += accel * Time.fixedDeltaTime;
        }
        Move();
        if ((Mathf.Abs(this.gameObject.transform.position.x) > lengthX + 1f) || ((Mathf.Abs(this.gameObject.transform.position.y) > lengthY + 1f)))
        {
            Destroy(this.gameObject);
        }
        accelTime += Time.fixedDeltaTime;
    }

    void Move()
    {   
        
        this.gameObject.transform.Translate(direction * moveSpeed * Time.fixedDeltaTime);
    }


}
