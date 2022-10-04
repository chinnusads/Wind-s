using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeControl : MonoBehaviour
{
    private float gaugeCount;

    public float upSpeed, downSpeed;//ゲージ上昇スピード、減少のスピード
    public float stopTime;//オーバーするときしばらく動けなくなる時間帯
    public int gaugeCharge; //ジャンプできる高さを記録：0ジャンプ不可；1一段階ジャンプ；2二段階ジャンプ;3三段階（オーバー）
    Image image;
    void Start()
    {
        gaugeCount = 0;
        gaugeCharge = 0;
        image = GetComponent<Image>();
    
    }

    
    void Update()
    {
        //ゲージ係数の計算
        {
            if (gaugeCount < 1)//ゲージが満たしていない
            {
                if (Input.GetKey(KeyCode.G))//入力すると
                    gaugeCount += Time.deltaTime * upSpeed; //ゲージ加算
                if (gaugeCount > 0.6)
                {
                    gaugeCharge = 2;//二段ジャンプ可
                }
                else if (gaugeCount > 0.2)
                {
                    gaugeCharge = 1;//一段ジャンプ可
                }
                else
                    gaugeCharge = 0;//ジャンプ不可
            }
            else
            {
                gaugeCount = 1;//1以上の場合、１にする
                gaugeCharge = 3;//３段階（オーバー）
            }


            if (gaugeCount > 0)//ゲージは自動的に減少する処理
            {
                if (gaugeCharge < 3)//爆発していない
                {
                    gaugeCount -= Time.deltaTime * downSpeed;
                    if (gaugeCharge == 2)//２段ジャンプ
                    {
                        if (gaugeCount < 0.6)
                            gaugeCount = 0.6f;
                    }
                    else if (gaugeCharge == 1)//１段ジャンプ
                    {
                        if (gaugeCount < 0.2)
                            gaugeCount = 0.2f;
                    }
                }
                else//爆発状態
				{
                    gaugeCount = 1;
				}

            }
            else
                gaugeCount = 0;//0になったらずっと0に保持する
        }
        //画像と連動する
        image.fillAmount = gaugeCount;

        //ジャンプすると一気に消耗する
        if (gaugeCharge <3)//爆発ではない状態
        {
            if (Input.GetKey(KeyCode.F))
            {
                gaugeCharge = 0;
                gaugeCount = 0;
            }
        }

        //３までチャージするとしばらく動けなくなる
        if(gaugeCharge ==3)
		{
            stopTime -= Time.deltaTime;
             if (stopTime < 0)//time out
			{
                gaugeCharge = 0;
                gaugeCount = 0;
            }
		}
        //問題点：stopTime一回しか使えない。繰り返す使えるように処理を欲しい。

    }
}
