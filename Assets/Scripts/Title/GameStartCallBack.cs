using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartCallBack : MonoBehaviour
{
    // Start is called before the first frame update
    public void onGameStart()
    {
        SceneManager.LoadScene(("inGame"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
