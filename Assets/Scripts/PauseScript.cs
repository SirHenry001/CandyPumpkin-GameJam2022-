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

    public PlayerInput playerInput;
    public PlayerController playerController;
    public MenuManager menuManager;

    public void RetryGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level1");
    }

    public void Start()
    {
        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
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
            playerInput.enabled = false;
            playerController.enabled = false;
        }

        else
        {;
            Time.timeScale = 1f;
            pauseText.SetActive(false);
            resumeButton.SetActive(false);
            retryButton.SetActive(false);
            quitButton.SetActive(false);
            backgroundImage.SetActive(false);
            playerInput.enabled = true;
            playerController.enabled = true;
        }
    }

    //FUNCTIONS TO DEAD % WIN CANVAS BELOW

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
        menuManager.deadCanvas.SetActive(false);
        playerController.canMove = true;
        playerInput.enabled = true;
        playerController.enabled = true;
    }

}
