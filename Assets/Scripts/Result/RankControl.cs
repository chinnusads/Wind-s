using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
public class RankControl : MonoBehaviour
{
    private float timer;
    public float maxTime;//ランク文字出現のタイミング
    private Text text;
    void Start()
    {
        text = GetComponent<Text>();
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //ランク文字の出現タイミングを制御する
        timer += Time.deltaTime;
        if (timer > maxTime)
        {
            text.enabled = true;
        }
    }
}
