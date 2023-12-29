using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    List<int> widths = new List<int>() { 568, 960, 1280, 1920 };
    List<int> heights = new List<int>() { 320, 540, 800, 1080 };
    public GameObject pauseMenu;
    public GameObject optionsDialog;
    public AudioMixer audioMixer;
   
    private void Awake()
    {
       
    }
    public void NewGame()
    {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Options()
    {
        pauseMenu.SetActive(false);
        optionsDialog.SetActive(true);
    }
    public void Quit()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            PlayerPrefs.SetInt("CurStage", SceneManager.GetActiveScene().buildIndex - 1);
            
        }

        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        
        
        Application.Quit();
        
    }
    public void PauseGame()
    {
        if (!optionsDialog.activeInHierarchy)
        {
            MyGameManager.Instance.PauseGame();
        }



    }
    public void ResumeGame()
    {
        MyGameManager.Instance.ResumeGame();
        pauseMenu.SetActive(false);
    }

    public void SetScreenSize(int idx)
    {
        bool fullscreen = Screen.fullScreen;
        int width = widths[idx];
        int height = heights[idx];
        Screen.SetResolution(width, height, fullscreen);
    }

    public void SetFullScreen(bool isFull)
    {
        Screen.fullScreen = isFull;
    }

    public void Back()
    {
        pauseMenu.SetActive(true);
        optionsDialog.SetActive(false);
    }
    public void ChangeMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVol", volume);
    }
    public void ChangeFXVolume(float volume)
    {
        audioMixer.SetFloat("FXVol", volume);
    }
}