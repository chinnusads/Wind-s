using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameController : MonoBehaviour
{
    public float lateTime;
    public float timer;
    public bool isCount;

    void Start()
    {
        isCount = true;
        timer = 0;
        lateTime = 62f;
    }

    void Update()
    {
        /*if (Input.GetKeyDown((KeyCode.Space)))
        {
            isCount = true;

        }*/

        if (isCount)
        {
            timer += Time.deltaTime;
        }
        if (timer > lateTime)
        {
            SceneManager.LoadScene("Result");
            timer = 0;
            isCount = false;
        }

    }
}
