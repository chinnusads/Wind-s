using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnGameStart : MonoBehaviour
{
    public void GameisStart()
    {
        
        SceneManager.LoadScene("Tutorial");
    }

   
}
