using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleFireBallController : MonoBehaviour
{
    private GameObject player;
    Vector3 direction;
    public float moveSpeed;
    public float lengthX, lengthY;
    private Vector3 pos;

    void Start()
    {
        player = GameObject.Find("PlayerColliderTall");
        Vector3 player_pos = new Vector3(player.transform.position.x, player.transform.position.y - 0.5f, player.transform.position.z);
        if (this.gameObject.transform.position.x > 0)
        {
            pos = new Vector3(this.gameObject.transform.position.x * -1, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        }
        else
        {
            pos = this.gameObject.transform.position;
        }
        direction = player_pos - pos;
        direction = direction.normalized;
    }

    private void FixedUpdate()
    {
        Move();
        if ((Mathf.Abs(this.gameObject.transform.position.x) > lengthX + 1f) || ((Mathf.Abs(this.gameObject.transform.position.y) > lengthY + 1f)))
        {
            Destroy(this.gameObject);
        }
    }

    void Move()
    {
        this.gameObject.transform.Translate(direction * moveSpeed * Time.fixedDeltaTime);
    }


}
