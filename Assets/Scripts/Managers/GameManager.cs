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

    //ACCESS TO OTHER SCRIPTS
    public HealthScipt playerHealth;
    public PlayerController playerController;

    public Animator myAnimator1;
    public Animator myAnimator2;
    public Animator myAnimator3;
    public Animator myAnimator4;
    public Animator myAnimator5;

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
}
