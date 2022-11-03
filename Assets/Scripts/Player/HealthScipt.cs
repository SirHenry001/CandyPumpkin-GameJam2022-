using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScipt : MonoBehaviour
{

    public int health = 5;

    //HEALTH IMAGE VARIABLES
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;

    public Animator myAnimatorH1;
    public Animator myAnimatorH2;
    public Animator myAnimatorH3;
    public Animator myAnimatorH4;
    public Animator myAnimatorH5;

    public Animator myAnimator;
    public Animator myAnimator2;

    public GameObject playerCollider;

    public MenuManager menuManager;
    public AudioManager audioManager;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        myAnimator = GetComponentInChildren<Animator>();
        myAnimator2 = GameObject.Find("Character2").GetComponent<Animator>();

        myAnimatorH1 = heart1.GetComponent<Animator>();
        myAnimatorH2 = heart2.GetComponent<Animator>();
        myAnimatorH3 = heart3.GetComponent<Animator>();
        myAnimatorH4 = heart4.GetComponent<Animator>();
        myAnimatorH5 = heart5.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (health == 4)
        {
            myAnimatorH5.SetTrigger("Broke");

        }

        if (health == 3)
        {
            myAnimatorH4.SetTrigger("Broke");

        }

        if (health == 2)
        {
            myAnimatorH3.SetTrigger("Broke");

        }

        if (health == 1)
        {
            myAnimatorH2.SetTrigger("Broke");

        }

        if (health <= 0)
        {
            myAnimatorH1.SetTrigger("Broke");
        }
    }

    public void Death()
    {
        print("osuma pelaajaan!");
        health -= 1;

        //ONLY HEAD PUMPKIN DEAD ANIMATION STARTS IF DIED
        if (health <= 0 && playerController.isLevel1 == true)
        {
            myAnimator.SetTrigger("Dead");
            GameObject.Find("Collider").GetComponent<CapsuleCollider2D>().enabled = false;
            audioManager.PlayHero(2);
            StartCoroutine(menuManager.DeadCanvas());
        }

        //FULL SIZED PUMPKIN DEAD ANIMATION STARTS IF DIED
        if (health <= 0 && playerController.isLevel1 == false)
        {
            myAnimator2.SetTrigger("Dead2");
            GameObject.Find("Collider").GetComponent<CapsuleCollider2D>().enabled = false;
            audioManager.PlayHero(2);
            StartCoroutine(menuManager.DeadCanvas());
        }

    }
}
