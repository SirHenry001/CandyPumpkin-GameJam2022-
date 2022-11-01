using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    //VARIABLE FOR LOGOS
    public GameObject logoText;
    public GameObject logoImage;

    //VARIABLES FOR BUTTONS
    public GameObject startButton;
    public GameObject playButton;
    public GameObject optionsButton;
    public GameObject aboutButton;
    public GameObject backButton;
    public GameObject quitButton;

    public GameObject fadeOutImage;
    public GameObject fadeInImage;

    //public ParticleSystem buttonEffects;
    public Animator myAnimator;

    public GameManager gameManager;
    public AudioManager audioManager;
    public AudioSource myAudio;


    // Start is called before the first frame update
    void Start()
    {

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        myAudio = audioManager.GetComponent<AudioSource>();

        fadeOutImage.SetActive(true);
        StartCoroutine(FadeOut());

        myAnimator = GameObject.Find("FadeOut").GetComponent<Animator>();
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

        logoImage.SetActive(false);
        quitButton.SetActive(false);
        playButton.SetActive(false);
        optionsButton.SetActive(false);
        aboutButton.SetActive(false);
        backButton.SetActive(true);
    }

    public void OpenAbout()
    {
        logoImage.SetActive(false);
        quitButton.SetActive(false);
        playButton.SetActive(false);
        optionsButton.SetActive(false);
        aboutButton.SetActive(false);
        backButton.SetActive(true);
    }

    public void Back()
    {
        logoText.SetActive(true);
        logoImage.SetActive(true);
        quitButton.SetActive(true);
        playButton.SetActive(true);
        optionsButton.SetActive(true);
        aboutButton.SetActive(true);
        backButton.SetActive(false);
    }



    public IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1f);
        fadeOutImage.SetActive(false);
    }

    public IEnumerator StartMenu()
    {
        startButton.GetComponent<Animator>().SetTrigger("Pressed");
        //buttonEffects.Play();
        audioManager.PlayMenu(1);
        yield return new WaitForSeconds(0.5f);
        startButton.SetActive(false);
        logoText.SetActive(true);
        logoImage.SetActive(true);
        quitButton.SetActive(true);
        playButton.SetActive(true);
        optionsButton.SetActive(true);
        aboutButton.SetActive(true);

    }

    public IEnumerator OpenLevelDelay()
    {
        audioManager.PlayMenu(2);
        fadeOutImage.SetActive(true);
        myAnimator.SetBool("FadeIn", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
