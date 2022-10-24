using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreatorController : MonoBehaviour
{
    public GameObject fireBallPrefab;
    public GameObject doubleFireBallPrefab;
    public GameObject rockPrefab;
    public GameObject batPrefab;
    public float intervalTime;
    private float waitTime;
    public  float lengthX, lengthY;
    private float newEnemyPositionX, newEnemyPositionY;
    private int enemyType;

    void Start()
    {
        waitTime = intervalTime;
    }

    void FixedUpdate()
    {
        if (waitTime >= intervalTime)
        {
            EnemyType();
            bornPosition();
            switch (enemyType)
            {
                case 1:
                    {
                        Instantiate(doubleFireBallPrefab, new Vector3(newEnemyPositionX, newEnemyPositionY, 0), Quaternion.Euler(0, 0, 0));
                        break;
                    }
                case 2:
                    {
                        Instantiate(fireBallPrefab, new Vector3(newEnemyPositionX, newEnemyPositionY - 0.3f, 0), Quaternion.Euler(0, 0, 0));
                        break;
                    }
                case 3:
                    {
                        Instantiate(rockPrefab, new Vector3(newEnemyPositionX, newEnemyPositionY - 0.3f, 0), Quaternion.Euler(0, 0, 0));
                        break;
                    }
                case 4:
                    {
                        Instantiate(batPrefab, new Vector3(newEnemyPositionX, newEnemyPositionY - 0.3f, 0), Quaternion.Euler(0, 0, 0));
                        break;
                    }
            } 
            waitTime = 0;
        }
        waitTime += Time.fixedDeltaTime;
    }

    void EnemyType()
    {
        enemyType = Random.Range(1, 5);
    }

    void bornPosition()
    {
        int quadrant;
        quadrant = Random.Range(1,7);
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
                    newEnemyPositionX = -lengthX;
                    newEnemyPositionY = lengthY;
                    break;
                }
            case 4:
                {
                    newEnemyPositionX = -lengthX;
                    newEnemyPositionY = 0;
                    break;
                }
            case 5:
                {
                    newEnemyPositionX = -lengthX;
                    newEnemyPositionY = -lengthY;
                    break;
                }
            case 6:
                {
                    newEnemyPositionX = lengthX;
                    newEnemyPositionY = -lengthY;
                    break;
                }
        }
    }
}

