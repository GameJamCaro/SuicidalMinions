using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject escapePanel;


    private void Start()
    {
        Resume();
    }


    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                if (escapePanel.activeInHierarchy)
                {
                    Resume();
                }
                else
                {
                    escapePanel.SetActive(true);
                    Time.timeScale = 0;
                }
            }
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("Level1");        
    }


    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Resume();
    }

    public void NextLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        Resume();
    }

    public void Resume() 
    {
        escapePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
