using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeControl : MonoBehaviour
{
    public float gaugeCount;
    public float upSpeed, downSpeed;//ゲージ上昇スピード、減少のスピード
    Image image;
    void Start()
    {
        gaugeCount = 0;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //ゲージ係数の計算
        {
            if (gaugeCount < 1)//ゲージが満たしていない
            {
                if (Input.GetKey(KeyCode.G))//入力すると
                    gaugeCount += Time.deltaTime*upSpeed; //ゲージ加算
            }
            else
                gaugeCount = 1;//1以上の場合、１にする


            if (gaugeCount > 0)//ゲージは自動的に減少する
            {
                gaugeCount -= Time.deltaTime * downSpeed;
            }
            else
                gaugeCount = 0;//０になったらずっと0にする
        }
        //画像と連動する
        image.fillAmount = gaugeCount;



    }
}
