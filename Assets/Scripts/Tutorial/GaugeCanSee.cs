using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeCanSee : MonoBehaviour
{
    Image image;
    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if((PlayerController.joyconCharge) || (Input.GetKey(KeyCode.G)))
		{
            image.enabled = true;
		}
    }
}
