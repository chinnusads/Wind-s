using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    Vector3 direction;
    public float moveSpeed;
    public float lengthX, lengthY;

    void Awake()
    {
        player = GameObject.Find("PlayerCollider");
        direction = player.transform.position - this.gameObject.transform.position;
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
