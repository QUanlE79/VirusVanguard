using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager
{
    // Start is called before the first frame update
    GameObject pauseMenu;
    private static MyGameManager _instance;
    public static MyGameManager Instance
    {
        get
        {
            if( _instance == null)
            {
                _instance = new MyGameManager();
            }
            return _instance;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        

    }
    public void ResumeGame()
    {
        Time.timeScale = 1 ;
    }
}
