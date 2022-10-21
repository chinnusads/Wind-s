using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InGameCameraControl : MonoBehaviour
{
    private float hight;
    public float states;
    private Camera cam;
    public float minPlayerHeight, maxPlayerHeight;
    private float minCameraSize, maxCameraSize;

    void Start()
    {
        cam = GetComponent<Camera>();
        minCameraSize = 4.6f;
        maxCameraSize = 5.4f;
    }

    // Update is called once per frame
    void Update()
    {
        hight = PlayerInfo.playerY*states;
        this.transform.position = new Vector3(this.transform.position.x, hight, this.transform.position.z);
        //0
        //6.112197
        //4.6
        //5.4
        cam.orthographicSize = (PlayerInfo.playerY/(maxPlayerHeight-minPlayerHeight) * (maxCameraSize -minCameraSize)) + minCameraSize;
    }
}
