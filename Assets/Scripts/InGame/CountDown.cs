using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    Text text;
    private float timer, count,timer2;
    public static bool isGameStart,isTimeOut;
    public AudioSource sceneChangeSE;
    public AudioSource countDownSE, startSE, timeOutSE;
    private int SETimes;

    void Start()
    {
        text = GetComponent<Text>();
        count = 3;
        text.enabled = true;
        isGameStart = false;
        isTimeOut = false;
        SETimes = 1;
        countDownSE.Play();
    }


    void Update()
    {
        timer += Time.deltaTime;
        count = 3 - timer;
        
        if ((count <=2) && (SETimes == 1))
        {
            countDownSE.Play();
            SETimes++;
        }
        if ((count <= 1) && (SETimes == 2))
        {
            countDownSE.Play();
            SETimes++;
        }
        if (count > 0.5)
        {
            text.text = count.ToString("0");
        }
        if ((count <= 0.5) && (count > 0))
        {
            startSE.Play();
            text.text = "Start";
        }
        if (count <= 0)
        {
            text.enabled = false;
            isGameStart = true;
        }
        if (isTimeOut)
        {
            timeOutSE.Play();
            text.enabled = true;
            text.text = "Time Out!";
            timer2 += Time.deltaTime;
            if (timer2 > 1)
            {
                sceneChangeSE.Play();
                SceneManager.LoadScene("Result");
            }
        }
    }
}
