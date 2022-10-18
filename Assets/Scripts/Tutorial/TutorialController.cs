using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    public float lateTime;
    public float timer;
    public bool isCount;
    void Start()
    {
        isCount = false;
        timer = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown((KeyCode.Space)))
        {
            isCount = true;

        }

        if (isCount)
        {
            timer += Time.deltaTime;
        }
        if (timer > lateTime)
        {
            SceneManager.LoadScene("InGame");
            timer = 0;
            isCount = false;
        }

    }
}