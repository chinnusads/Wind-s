using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderController : MonoBehaviour
{
    [SerializeField]private bool hasPlayer, hasEnemy;
    public bool getHitted,getavoided;

    void Awake()
    {
        hasEnemy = false;
        hasPlayer = false;
        getHitted = false;
        getavoided = false;
    }

    void Update()
    {
        if (getHitted)//当たった
        {
            getHitted = false;
            Debug.Log("hitted");
            ScoreControl.scoreCountDown++; //score計算
        }

        if (getavoided)//避けた
        {
            getavoided = false;
            Debug.Log(("avoided"));
            ScoreControl.scoreCountUp++;//score計算
        }
        if ((hasPlayer) && (hasEnemy))　//当たる判定
        {
            getHitted = true;
            hasEnemy = false;
        }
        if ((!hasPlayer) && (hasEnemy))//避ける判定
        {
            getavoided = true;
            hasEnemy = false;
        }

        
    }   

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Player":
                {
                    hasPlayer = true;
                    break;
                }
            case "Enemy":
                {
                    hasEnemy = true;
                    break;
                }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Player":
                {
                    hasPlayer = false;
                    break;
                }
            case "Enemy":
                {
                    hasEnemy = false;
                    break;
                }
        }
    }
}
