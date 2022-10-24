using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScaleController : MonoBehaviour
{

    void Start()
    {
        if (this.gameObject.transform.position.x > 0)
        {
            this.gameObject.transform.localScale = new Vector3(-1, this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
