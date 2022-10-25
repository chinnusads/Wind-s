using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour
{
    private GameObject player;
    Vector3 direction;
    public float moveSpeed;
    public float lengthX, lengthY;
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
    }

    void FixedUpdate()
    {
        if ((CountDown.isGameStart) && (!CountDown.isTimeOut))
        {
            Move();
            if ((Mathf.Abs(this.gameObject.transform.position.x) > lengthX + 1f) || ((Mathf.Abs(this.gameObject.transform.position.y) > lengthY + 1f)))
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Move()
    {
        this.gameObject.transform.Translate(direction * moveSpeed * Time.fixedDeltaTime);
    }

    
}
