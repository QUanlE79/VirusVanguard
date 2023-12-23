using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pauseMenu;
    private void Awake()
    {
        
    }
    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Options()
    {

    }
    public void Quit()
    {
        Application.Quit();
    }
    public void PauseGame()
    {
        MyGameManager.Instance.PauseGame();
        
        
        
    }
    public void ResumeGame()
    {
        MyGameManager.Instance.ResumeGame();
        pauseMenu.SetActive(false);
    }

}
