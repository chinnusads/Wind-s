using System.Collections;
using System.Collections.Generic;
using Nissensai2022.Runtime;
using UnityEngine;

public class SendRank : MonoBehaviour
{
    private ResultRank rank;
    void Start()
    {
        rank = ResultRank.A;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown((KeyCode.Space)))
        {
            Nissensai.SendResult(rank);
        }
    }
}
