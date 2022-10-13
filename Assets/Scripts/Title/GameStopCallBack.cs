using System.Collections;
using System.Collections.Generic;
using Nissensai2022.Runtime;
using UnityEngine;
using ZXing;
using UnityEngine.SceneManagement;



public class GameStopCallBack : MonoBehaviour
{
    // Start is called before the first frame update
    public void GameStop()
    {
        /*ResultRank result= ResultRank.A;
        Nissensai.SendResult(result);*/
        SceneManager.LoadScene("Title");
    }

    
}
