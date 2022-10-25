using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderShortController : MonoBehaviour
{
    [SerializeField]private bool hasPlayer, hasEnemy;
    private bool getHitted,getavoided,deleted;

    public GameObject attackedUI;
    public GameObject player;
    public GameObject ExpPlus;

    void Start()
    {
        hasEnemy = false;
        hasPlayer = false;
        getHitted = false;
        getavoided = false;
        deleted = false;
    }

    void Update()
    {
        if ((getHitted) && (deleted))//当たった
        {
            getHitted = false;
            deleted = false;
            Debug.Log("hitted");
            //Instantiate(ExpPlus, this.transform.position, this.transform.rotation);
            ScoreControl.scoreCountDown++; //score計算
        }
        if (getavoided)//避けた
        {
            getavoided = false;
            Debug.Log(("avoided"));
            Instantiate(ExpPlus, new Vector3(PlayerInfo.playerX,PlayerInfo.playerY,0f), this.transform.rotation);
            ScoreControl.scoreCountUp++;//score計算
        }
        if ((hasPlayer) && (hasEnemy))　//当たる判定
        {
            getHitted = true;
            deleted = false;
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

    private void OnTriggerStay2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Enemy":
                {
                    if ((getHitted) && (!deleted))
                    {
                        Destroy(col.gameObject);
                        deleted = true;
                        var hasUI = GameObject.Find("PlayerAttacked");
                        if (hasUI!=null)
                        {
                            Destroy(hasUI);
                        }
                        Instantiate(attackedUI);
                    }
                    break;
                }
        }
    }
}
