using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPlus : MonoBehaviour
{
    SpriteRenderer sprd;
    private float timer;
    void Start()
    {
        sprd = GetComponent<SpriteRenderer>();
        timer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(0f, 0.1f, 0f);
        if(timer >1)
		{
            Destroy(this.gameObject);
		}            
    }
}
