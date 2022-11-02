using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    //VARIABLE FOR TEXTS
    public GameObject logoText;
    public GameObject anykeyText;
    public GameObject jamText;
    public GameObject aboutText;
    public GameObject helpText;

    //VARIABLES FOR CANVAS
    public GameObject deadCanvas;
    public GameObject winCanvas;

    //VARIABLES FOR BUTTONS
    public GameObject startButton;
    public GameObject playButton;
    public GameObject optionsButton;
    public GameObject aboutButton;
    public GameObject backButton;
    public GameObject quitButton;

    //VARIABLES FOR IMAGES
    public GameObject fadeOutImage;
    public GameObject startImage;
    public GameObject popupImage;
    public GameObject levelChangeImage;

    //public ParticleSystem buttonEffects;
    public Animator myAnimator;

    public GameManager gameManager;
    public AudioManager audioManager;
    public PlayerInput playerInput;
    public PlayerController playerController;
    public AudioSource myAudio;

    public bool gameActive = false;

    private void Awake()
    {
        fadeOutImage.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {


        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        myAudio = audioManager.GetComponent<AudioSource>();

        fadeOutImage.SetActive(true);
        StartCoroutine(FadeOut());

        myAnimator = GameObject.Find("FadeOut").GetComponent<Animator>();


        myAnimator.SetBool("FadeIn", false);

    }

    public void Update()
    {
        if(Input.anyKey && !gameActive)
        {
            StartCoroutine(StartMenu());
        }
    }

    public void MainStart()
    {
        StartCoroutine(StartMenu());
    }

    public void OpenLevel()
    {

        StartCoroutine(OpenLevelDelay());
    }

    public void OpenOptions()
    {

        quitButton.SetActive(false);
        playButton.SetActive(false);
        optionsButton.SetActive(false);
        aboutButton.SetActive(false);
        backButton.SetActive(true);
        helpText.SetActive(true);
        logoText.SetActive(false);
        popupImage.SetActive(true);


    }

    public void OpenAbout()
    {
        quitButton.SetActive(false);
        playButton.SetActive(false);
        optionsButton.SetActive(false);
        aboutButton.SetActive(false);
        backButton.SetActive(true);
        aboutText.SetActive(true);
        popupImage.SetActive(true);
        logoText.SetActive(false);
    }

    public void Back()
    {
        quitButton.SetActive(true);
        playButton.SetActive(true);
        optionsButton.SetActive(true);
        aboutButton.SetActive(true);
        backButton.SetActive(false);
        aboutText.SetActive(false);
        popupImage.SetActive(false);
        logoText.SetActive(true);
        helpText.SetActive(false);
    }



    public IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1f);
        fadeOutImage.SetActive(false);
    }

    public IEnumerator StartMenu()
    {
        startImage.GetComponent<Animator>().SetBool("Start",true);
        audioManager.PlayMenu(1);
        gameActive = true;
        fadeOutImage.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        startButton.SetActive(false);
        anykeyText.SetActive(false);
        startImage.SetActive(false);
        quitButton.SetActive(true);
        playButton.SetActive(true);
        optionsButton.SetActive(true);
        aboutButton.SetActive(true);
        jamText.SetActive(true);

    }

    public IEnumerator OpenLevelDelay()
    {
        audioManager.PlayMenu(2);
        fadeOutImage.SetActive(true);
        myAnimator.SetBool("FadeIn", true);

        print("läpi");

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level1");
    }


    public IEnumerator DeadCanvas()
    {
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0f;
        deadCanvas.SetActive(true);
        playerController.canMove = false;
        playerInput.enabled = false;
        playerController.enabled = false;
    }

    public IEnumerator WinCanvas()
    {
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0f;
        winCanvas.SetActive(true);
        playerController.canMove = false;
        playerInput.enabled = false;
        playerController.enabled = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
