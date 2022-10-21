using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logger = Nissensai2022.Runtime.Logger;

public class DanmakuDemoProvider : MonoBehaviour
{
    private int i = 0;

    private float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1f)
        {
            timer = 0f;
            Nissensai2022.Danmaku.Danmaku.AddDanmaku((++i).ToString(),true,1);
        }
        else
        {
            Nissensai2022.Danmaku.Danmaku.AddDanmaku((++i).ToString());
        }
    }
}