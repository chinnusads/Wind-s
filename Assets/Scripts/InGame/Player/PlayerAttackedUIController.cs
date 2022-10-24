using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackedUIController : MonoBehaviour
{
    public float maxLiveTime;
    private float liveTime;
    private GameObject player;

    void Start()
    {
        liveTime = 0f;
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        liveTime += Time.deltaTime;
        if (liveTime > maxLiveTime)
        {
            Destroy(this.gameObject);
        }
        this.gameObject.transform.position = new Vector3(player.transform.position.x+0.1f, player.transform.position.y+0.2f, player.transform.position.z);
    }
}
