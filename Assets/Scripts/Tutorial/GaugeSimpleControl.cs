using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

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
        if ((PlayerController.joyconCharge) || (Input.GetKey(KeyCode.G)))//���͌��m
        {
            gaugeCount += Time.deltaTime * upSpeed; //�Q�[�W�㏸
        }

        image.fillAmount = gaugeCount;

        if(gaugeCount >= 1)
		{
            TutorialController.isCount = true;
		}
        
    }
}
