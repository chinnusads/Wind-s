using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;
using Nissensai2022.Runtime;

public class ResultSceneSwitch : MonoBehaviour
{
    public float timer;

    private bool isstop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(!isstop)
        {
            if (timer < 0)
            {
                ResultRank result= ResultRank.A;
                Nissensai.SendResult(result);
                isstop = true;
            }
            
        }
    }
}
