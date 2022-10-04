using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderController : MonoBehaviour
{
    [SerializeField]private bool hasPlayer, hasEnemy;
    public bool getHitted;

    void Awake()
    {
        hasEnemy = false;
        hasPlayer = false;
        getHitted = false;
    }

    void Update()
    {
        if (getHitted)
        {
            getHitted = false;
            Debug.Log("hitted");
        }
        if ((hasPlayer) && (hasEnemy))
        {
            getHitted = true;
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
