using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static float playerY;//プレイヤーのy軸座標を取得する⇒カメラの運動につながる
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerY = this.transform.position.y;
    }
}
