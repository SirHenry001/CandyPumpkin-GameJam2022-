using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{

    public static MenuManager menuManager;

    //VARIABLE FOR TEXTS
    public GameObject logoText;
    public GameObject anykeyText;
    public GameObject jamText;
    public GameObject aboutText;
    public GameObject helpText;
    public GameObject levelChangeText;

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
    public PlayerInput playerInput;
    public PlayerController playerController;
    public AudioManager audioManager;
    public AudioSource myAudio;

    public bool gameActive = false;

    private void Awake()
    {
        fadeOutImage.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {


        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();    
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        myAudio = audioManager.GetComponent<AudioSource>();

        fadeOutImage.SetActive(true);
        StartCoroutine(FadeOut());

        myAnimator = GameObject.Find("FadeOut").GetComponent<Animator>();


        myAnimator.SetBool("FadeIn", false);



        if (gameManager == null)
        {
            //gamemangeri ei tuhoudu scenejen välillä!!
            DontDestroyOnLoad(gameObject);
            menuManager = this;
        }

        else if (menuManager != null)
        {
            Destroy(gameObject);
        }

    }

    public void Update()
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == "MenuScreen")
        {
            if (Input.anyKey && !gameActive)
            {
                audioManager.PlayEffects(1);
                MainStart();
            }
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
        audioManager.PlayEffects(2);


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
        audioManager.PlayEffects(2);
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
        audioManager.PlayEffects(2);
    }



    public IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1f);
        fadeOutImage.SetActive(false);
    }

    public IEnumerator StartMenu()
    {
        startImage.GetComponent<Animator>().SetBool("Start",true);
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
        audioManager.PlayEffects(2);
        fadeOutImage.SetActive(true);
        myAnimator.SetBool("FadeIn", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level1");
    }


    public IEnumerator DeadCanvas()
    {
        playerController.myRigidbody.velocity = Vector2.zero;
        deadCanvas.SetActive(true);
        playerController.canMove = false;
        playerInput.enabled = false;
        playerController.enabled = false;
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0f;

    }

    public IEnumerator WinCanvas()
    {
        playerController.myAnimator.SetTrigger("End");
        playerController.myAnimator2.SetTrigger("End2");
        playerController.myRigidbody.velocity = Vector2.zero;
        playerController.canMove = false;
        playerInput.enabled = false;
        playerController.enabled = false;
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
        winCanvas.SetActive(true);

    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
