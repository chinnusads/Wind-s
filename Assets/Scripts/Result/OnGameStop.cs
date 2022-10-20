using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Nissensai2022.Runtime;
using ZXing;

public class OnGameStop : MonoBehaviour
{

    public void GameisStop()
    {
        SceneManager.LoadScene("Title");
    }
    
}
