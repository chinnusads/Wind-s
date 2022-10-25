using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GaugeSimpleControl : MonoBehaviour
{
    Image image;
    private float gaugeCount,timer;

    public float upSpeed;

	void Start()
    {
        image = GetComponent<Image>();
        image.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((TutorialController.joyconCharge) || (Input.GetKey(KeyCode.G)))
        {
            gaugeCount += Time.deltaTime * upSpeed; 
        }

        image.fillAmount = gaugeCount;

        if(gaugeCount >= 1)
		{
            if (TutorialController.joyconBottun)
            {
                SceneManager.LoadScene("InGame");
            }
		}
        
    }
}
