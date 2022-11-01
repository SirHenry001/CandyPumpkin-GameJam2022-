using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseText;
    public GameObject resumeButton;
    public GameObject retryButton;
    public GameObject quitButton;
    public GameObject backgroundImage;



    public void RetryGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level1");
    }

    public void Start()
    {

    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        pauseText.SetActive(false);
        resumeButton.SetActive(false);
        retryButton.SetActive(false);
        quitButton.SetActive(false);
        backgroundImage.SetActive(false);
    }

    public void Mainmenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScreen");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            pauseText.SetActive(true);
            resumeButton.SetActive(true);
            retryButton.SetActive(true);
            quitButton.SetActive(true);
            backgroundImage.SetActive(true);
        }

        else
        {;
            Time.timeScale = 1f;
            pauseText.SetActive(false);
            resumeButton.SetActive(false);
            retryButton.SetActive(false);
            quitButton.SetActive(false);
            backgroundImage.SetActive(false);
        }
    }
}
