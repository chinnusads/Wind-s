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

    void Awake()
    {
        player = GameObject.Find("PlayerColliderShort");
        direction = player.transform.position - this.gameObject.transform.position;
        direction = direction.normalized;
        moveSpeed = startSpeed;
        accelTime = 0f;
    }

    private void FixedUpdate()
    {
        if (accelTime > 1f)
        {
            moveSpeed += accel * Time.fixedDeltaTime;
        }
        Move();
        if ((Mathf.Abs(this.gameObject.transform.position.x) >= lengthX) || ((Mathf.Abs(this.gameObject.transform.position.y) >= lengthY)))
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
