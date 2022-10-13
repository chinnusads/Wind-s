using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleFireBallController : MonoBehaviour
{
    private GameObject player;
    Vector3 direction;
    public float moveSpeed;
    public float lengthX, lengthY;

    void Awake()
    {
        player = GameObject.Find("PlayerColliderTall");
        Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y - 0.5f, player.transform.position.z);
        direction = pos - this.gameObject.transform.position;
        direction = direction.normalized;
    }

    private void FixedUpdate()
    {
        Move();
        if ((Mathf.Abs(this.gameObject.transform.position.x) >= lengthX) || ((Mathf.Abs(this.gameObject.transform.position.y) >= lengthY)))
        {
            Destroy(this.gameObject);
        }
    }

    void Move()
    {
        this.gameObject.transform.Translate(direction * moveSpeed * Time.fixedDeltaTime);
    }


}
