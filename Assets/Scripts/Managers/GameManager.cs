using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager;


    //PROGRESS BAR VARIABLES
    public Image progressBar;
    public Image progressBar2;

    public float candyCollected = 0;
    public float candyCollected2 = 0;

    public int difficultLevel = 1;

    //ACCESS TO OTHER SCRIPTS
    public HealthScipt playerHealth;
    public PlayerController playerController;
    public MenuManager menuManager;
    public AudioManager audioManager;
    public PlayerInput playerInput;
    public EnemySpawner enemySpawner;

    public bool levelChange;
    public bool gameEnd;


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
        playerHealth = GameObject.Find("Player").GetComponent<HealthScipt>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        progressBar = GameObject.Find("ProgressBar").GetComponent<Image>();
        progressBar2 = GameObject.Find("ProgressBar2").GetComponent<Image>();
;
    }

    // Start is called before the first frame update
    void Start()
    {

        gameEnd = false;

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
        playerHealth = GameObject.Find("Player").GetComponent<HealthScipt>();   
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        progressBar = GameObject.Find("ProgressBar").GetComponent<Image>();
        progressBar2 = GameObject.Find("ProgressBar2").GetComponent<Image>();


        if (gameManager == null)
        {
            //gamemangeri ei tuhoudu scenejen välillä!!
            DontDestroyOnLoad(gameObject);
            gameManager = this;
        }

        else if(gameManager != null )
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FixedUpdate()
    {
        
    }

    public void FillCandy()
    {
        

        if(difficultLevel == 1)
        {

            candyCollected += 10;
            progressBar.fillAmount = candyCollected * 0.01f;

            if (candyCollected >= 100)
            {
                candyCollected = 100;
                difficultLevel = 0;
                StartCoroutine(LevelChange());
            }

        }

        if(difficultLevel == 2)
        {
            candyCollected2 += 10;

            progressBar2.fillAmount = candyCollected2 * 0.01f;

            if (candyCollected2 >= 100)
            {
                candyCollected2 = 100;
                gameEnd = true;
                StartCoroutine(GameWin());
            }

        }


    }

    public IEnumerator LevelChange()
    {
        levelChange = true;
        GameObject.Find("Collider").GetComponent<CapsuleCollider2D>().enabled = false;
        menuManager.levelChangeImage.SetActive(true);
        playerController.xSpeed = 6;
        playerController.ySpeed = 3;
        playerController.enabled = false;
        playerInput.enabled = false;
        playerController.myRigidbody.velocity = Vector2.zero;
        audioManager.PlayEffects(0);
        yield return new WaitForSeconds(1f);
        menuManager.levelChangeText.SetActive(true);
        playerController.isLevel1 = false;
        yield return new WaitForSeconds(4f);
        levelChange = false;
        difficultLevel = 2;
        playerController.enabled = true;
        playerInput.enabled = true;
        menuManager.levelChangeText.SetActive(false);
        menuManager.levelChangeImage.SetActive(false);
        audioManager.PlayEffects(0);
        GameObject.Find("Collider").GetComponent<CapsuleCollider2D>().enabled = true;
    }

    public IEnumerator GameWin()
    {
        print("voitit");
        gameEnd = true;
        yield return new WaitForSeconds(1f);
        playerController.enabled = false;
        playerInput.enabled = false;
        playerController.myRigidbody.velocity = Vector2.zero;
        yield return new WaitForSeconds(2f);
        StartCoroutine(menuManager.WinCanvas());

    }

    

}
