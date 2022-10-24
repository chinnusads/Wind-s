using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class TimeCountControl : MonoBehaviour
{
    public float maxTime;//カウントダウン時間
    private float timer;//経過時間
    private bool isCount;//時間をカウントすべきかどうか
    private Text text;

    
    void Start()
    {
        timer = 0;
        text = GetComponent<Text>();
        isCount = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //時間を表示する
        text.text = (maxTime - timer).ToString("00");
        
        if (isCount)//カウントダウンし始めたら
        {
            if (timer<maxTime)//時間はMaxではない
            {
                timer += Time.deltaTime; //経過時間計算
            }
            else
            {
                timer = maxTime; //時間は0以下にしないように
                isCount = false;
            }
            
        }
        
        //カウントダウンすべきフラグ
        if (CountDown.isGameStart)
        {
            isCount = true;
            
        }
        
        
        
        
        
        
    }
}
