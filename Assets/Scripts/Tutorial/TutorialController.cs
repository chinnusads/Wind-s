using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    public float lateTime;
    public float timer;
    public static bool isCount;
    //public GameObject WindSimple;
    void Start()
    {
        isCount = false;
        timer = 0;
        //isIstan = false;
    }

    void Update()
    {
        /*if (Input.GetKeyDown((KeyCode.Space)))
        {
            isCount = true;

        }*/

        if (isCount)
        {
            /*if (!isIstan)
            {
                //Instantiate(WindSimple, new Vector3(-30,107,0), this.transform.rotation);
                isIstan = true;
            }*/
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
