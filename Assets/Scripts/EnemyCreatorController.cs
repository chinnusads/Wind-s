using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreatorController : MonoBehaviour
{
    public GameObject enemyPrefab;
    Vector3 position;
    public int enemyTotalNum;
    private int enemyCount;
    public float intervalTime;
    private float waitTime;
    public  float lengthX, lengthY;
    private float newEnemyPositionX, newEnemyPositionY;

    void Awake()
    {
        waitTime = 0f;
        enemyCount = 0;
    }

    private void FixedUpdate()
    {
        if (enemyCount < enemyTotalNum)
        {
            if (waitTime >= 2f)
            {
                bornPosition();
                Instantiate(enemyPrefab, new Vector3(newEnemyPositionX, newEnemyPositionY, 0), Quaternion.Euler(0, 0, 0));
                enemyCount++;   
                waitTime = 0;
            }
        }
        waitTime += Time.fixedDeltaTime;
    }

    void bornPosition()
    {
        int quadrant;
        quadrant = Random.Range(1,9);
        switch (quadrant)
        {
            case 1:
                {
                    newEnemyPositionX = lengthX;
                    newEnemyPositionY = 0;
                    break;
                }
            case 2:
                {
                    newEnemyPositionX = lengthX;
                    newEnemyPositionY = lengthY;
                    break;
                }
            case 3:
                {
                    newEnemyPositionX = 0;
                    newEnemyPositionY = lengthY;
                    break;
                }
            case 4:
                {
                    newEnemyPositionX = -lengthX;
                    newEnemyPositionY = lengthY;
                    break;
                }
            case 5:
                {
                    newEnemyPositionX = -lengthX;
                    newEnemyPositionY = 0;
                    break;
                }
            case 6:
                {
                    newEnemyPositionX = -lengthX;
                    newEnemyPositionY = -lengthY;
                    break;
                }
            case 7:
                {
                    newEnemyPositionX = 0;
                    newEnemyPositionY = -lengthY;
                    break;
                }
            case 8:
                {
                    newEnemyPositionX = lengthX;
                    newEnemyPositionY = -lengthY;
                    break;
                }
        }
    }
}

