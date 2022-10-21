using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeControl : MonoBehaviour
{
    public static float gaugeCount;
    private float stopCount;
    public float upSpeed, downSpeed;//ゲージ上昇、落下のスピード
    public float stopTime;//ジャンプ３まで貯まるとしばらく動けなくなる
    public static int gaugeCharge; //ジャンプの状態。0ジャンプ不可；1一段階ジャンプでき：２二段階ジャンプでき；３貯まりすぎ動けなくなる
    Image image;
    
    
    void Start()
    {
        gaugeCount = 0;
        gaugeCharge = 0;
        image = GetComponent<Image>();
        stopCount = stopTime;
    }
    
    void Update()
    {
        //ゲージ上昇時の段階分け
        if (gaugeCount < 1)//ゲージ未満
        {
            if (((PlayerController.joyconCharge)||(Input.GetKey(KeyCode.G))) && (PlayerController.jumpState == 0))//入力検知
                gaugeCount += Time.deltaTime * upSpeed; //ゲージ上昇
            if (gaugeCount > 0.6)
            {
                gaugeCharge = 2;//2段階ジャンプでき
            }
            else if (gaugeCount > 0.2)
            {
                gaugeCharge = 1;//1段階ジャンプでき
            }
            else
                gaugeCharge = 0;//ジャンプ不可
        }
        else
        {
            gaugeCount = 1;//ゲージ満タン
            gaugeCharge = 3;//3段階
        }
        if (gaugeCount > 0)//落下の判定
        {
            if (gaugeCharge < 3)//ゲージ未満
            {
                gaugeCount -= Time.deltaTime * downSpeed;
                if (gaugeCharge == 2)//2段階まで
                {
                    if (gaugeCount < 0.6)
                        gaugeCount = 0.6f;
                }
                else if (gaugeCharge == 1)//1段階まで
                {
                    if (gaugeCount < 0.2)
                        gaugeCount = 0.2f;
                }
            }
            else//ゲージ満タン
			{
                gaugeCount = 1;
			}
        }
        else
            gaugeCount = 0;//0の時は落下しない
        //ゲージの画像表示
        image.fillAmount = gaugeCount;

        //ボタン押したらゲージを消耗する
        /*if ((gaugeCharge <3)&&(gaugeCharge>0))//満タンではない状態・ジャンプできる状態⇒ジャンプできる状態
        {
            if(PlayerController.canJump)//プレイヤーは空中ではない
            {
                if ((Input.GetKeyDown(KeyCode.Space)) || (JoyconInput.jump))
                {
                    if(gaugeCharge == 2)//2段ジャンプ
                    {
                        gaugeCount = gaugeCount - 0.6f;
                        PlayerController.isJump2 = true;
                    }
                    else if (gaugeCharge == 1)//1段ジャンプ
                    {
                        gaugeCount =gaugeCount - 0.2f;
                        PlayerController.isJump1 = true;
                    }
                gaugeCharge = 0;//ジャンプ不可
                }
            }
        }*/
        //満タンになる状態
        if (gaugeCharge ==3)
		{
            stopCount -= Time.deltaTime;//しばらく動けなくなる
            if (stopCount < 0)　//time out
			{
                gaugeCharge = 0;
                gaugeCount = 0;
                stopCount = stopTime;
            }
		}
        //改善可能の点：ゲージは一瞬で落ちるではなく、徐々に落ちで行く。

    }
}
