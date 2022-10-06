using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InGameCameraControl : MonoBehaviour
{
    private float hight;
    public float states;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hight = PlayerInfo.playerY*states;
        this.transform.position = new Vector3(this.transform.position.x, hight, this.transform.position.z);
    }
}
