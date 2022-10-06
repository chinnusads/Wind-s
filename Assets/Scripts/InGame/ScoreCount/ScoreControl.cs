using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreControl : MonoBehaviour
{
    public static float scoreCountUp,scoreCountDown;//加点計算、減点計算
    private Text text;
    public float statusUp,statusDown;//加点倍率、減点倍率
    void Start()
    {
        text = GetComponent<Text>();
        scoreCountDown = scoreCountUp = 0;
    }

    
    void Update()
    {
        
        //スコアを表示する：加点x加点倍率ー減点x減点倍率
        text.text = (scoreCountUp*statusUp-scoreCountDown*statusDown).ToString("0000"); 
        
    }
}
