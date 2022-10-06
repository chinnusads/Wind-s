using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static float playerY;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerY = this.transform.position.y;
    }
}
