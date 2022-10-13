using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderTallController : MonoBehaviour
{
    [SerializeField] private bool hasPlayer, hasEnemy;
    public bool getHitted, getavoided;

    void Awake()
    {
        hasEnemy = false;
        hasPlayer = false;
        getHitted = false;
        getavoided = false;
    }

    void Update()
    {
        if (getHitted)//�����ä�
        {
            getHitted = false;
            Debug.Log("hitted");
            ScoreControl.scoreCountDown++; //scoreӋ��
        }

        if (getavoided)//�ܤ���
        {
            getavoided = false;
            Debug.Log(("avoided"));
            ScoreControl.scoreCountUp++;//scoreӋ��
        }
        if ((hasPlayer) && (hasEnemy))��//�������ж�
        {
            getHitted = true;
            hasEnemy = false;
        }
        if ((!hasPlayer) && (hasEnemy))//�ܤ����ж�
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
            case "DoubleFireBall":
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
            case "DoubleFireBall":
                {
                    hasEnemy = false;
                    break;
                }
        }
    }
}
