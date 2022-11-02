using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager;

    //HEALTH IMAGE VARIABLES
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;

    //PROGRESS BAR VARIABLES
    public Image ProgressBar;
    public Image ProgressBar2;

    public float candyCollected = 0;
    public float candyCollected2 = 0;

    public int difficultLevel = 1;

    //ACCESS TO OTHER SCRIPTS
    public HealthScipt playerHealth;
    public PlayerController playerController;
    public MenuManager menuManager;
    public PlayerInput playerInput;
    public EnemySpawner enemySpawner;

    public Animator myAnimator1;
    public Animator myAnimator2;
    public Animator myAnimator3;
    public Animator myAnimator4;
    public Animator myAnimator5;

    public bool levelChange;
    public bool gameEnd;

    // Start is called before the first frame update
    void Start()
    {

        myAnimator1 = heart1.GetComponent<Animator>();
        myAnimator2 = heart2.GetComponent<Animator>();
        myAnimator3 = heart3.GetComponent<Animator>();
        myAnimator4 = heart4.GetComponent<Animator>();
        myAnimator5 = heart5.GetComponent<Animator>();

        playerHealth = GameObject.Find("Player").GetComponent<HealthScipt>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();

        if(gameManager == null)
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
        if(playerHealth.health == 4)
        {
            myAnimator5.SetTrigger("Broke");
            
        }

        if (playerHealth.health == 3)
        {
            myAnimator4.SetTrigger("Broke");

        }

        if (playerHealth.health == 2)
        {
            myAnimator3.SetTrigger("Broke");

        }

        if (playerHealth.health == 1)
        {
            myAnimator2.SetTrigger("Broke");

        }

        if (playerHealth.health == 0)
        {
            myAnimator1.SetTrigger("Broke");
            playerHealth.Death();

        }
    }

    public void FixedUpdate()
    {
        
    }

    public void FillCandy()
    {
        

        if(difficultLevel == 1)
        {

            candyCollected += 10;
            ProgressBar.fillAmount = candyCollected * 0.01f;

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

            ProgressBar2.fillAmount = candyCollected2 * 0.01f;

            if (candyCollected2 >= 100)
            {
                candyCollected2 = 100;
                gameEnd = true;
            }

        }
    }

    public IEnumerator LevelChange()
    {
        levelChange = true;
        menuManager.levelChangeImage.SetActive(true);
        yield return new WaitForSeconds(1f);
        playerController.xSpeed = 6f;
        playerController.ySpeed = 3f;
        playerController.isLevel1 = false;
        playerController.enabled = false;
        playerInput.enabled = false;
        playerController.myRigidbody.velocity = Vector2.zero;
        yield return new WaitForSeconds(4f);
        levelChange = false;
        difficultLevel = 2;
        playerController.enabled = true;
        playerInput.enabled = true;
        menuManager.levelChangeImage.SetActive(false);
    }

}
