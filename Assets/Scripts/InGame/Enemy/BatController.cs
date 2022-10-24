using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    private GameObject player;
    Vector3 direction;
    public float startSpeed;
    public float accel;
    public float lengthX, lengthY;
    private float moveSpeed;
    [SerializeField]private bool u_turn;
    [SerializeField]private int startQuadrant;
    [SerializeField]private int nowQuadrant;
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
        u_turn = false;
        startQuadrant = Quadrant();
    }

    private void FixedUpdate()
    {
        nowQuadrant = Quadrant();
        if ((nowQuadrant + 2 == startQuadrant) || (nowQuadrant - 2 == startQuadrant))
        {
            u_turn = true;
        }
        if (u_turn)
        {
            SpeedChange();
        }
        Move();
        if ((Mathf.Abs(this.gameObject.transform.position.x) > lengthX + 1f) || ((Mathf.Abs(this.gameObject.transform.position.y) > lengthY + 1f)))
        {
            Destroy(this.gameObject);
        }
    }

    int Quadrant()
    {
       if ((this.gameObject.transform.position.x >= 0f) && (this.gameObject.transform.position.y >= -0.3f))
        {
            return 1;
        }
       else if ((this.gameObject.transform.position.x < 0f) && (this.gameObject.transform.position.y > -0.3f))
        {
            return 2;
        }
       else if ((this.gameObject.transform.position.x <= 0f) && (this.gameObject.transform.position.y <= -0.3f))
        {
            return 3;
        }
       else if ((this.gameObject.transform.position.x > 0f) && (this.gameObject.transform.position.y < -0.3f))
        {
            return 4;
        }
        return 0;
    }

    void SpeedChange()
    {
        moveSpeed-=accel * Time.fixedDeltaTime;
        if (Mathf.Abs(moveSpeed) > startSpeed)
        {
            moveSpeed = -startSpeed;
            u_turn = false;
        }
    }

    void Move()
    {
        this.gameObject.transform.Translate(direction * moveSpeed * Time.fixedDeltaTime);
    }
}
