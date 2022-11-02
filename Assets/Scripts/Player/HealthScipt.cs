using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScipt : MonoBehaviour
{

    public int health = 5;

    public Animator myAnimator;
    public Animator myAnimator2;

    public MenuManager menuManager;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        myAnimator = GetComponentInChildren<Animator>();
        myAnimator2 = GameObject.Find("Character2").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Death()
    {

        health -= 1;

        //ONLY HEAD PUMPKIN DEAD ANIMATION STARTS IF DIED
        if (health <= 0 && playerController.isLevel1 == true)
        {
            myAnimator.SetTrigger("Dead");
            StartCoroutine(menuManager.DeadCanvas());
        }

        //FULL SIZED PUMPKIN DEAD ANIMATION STARTS IF DIED
        if (health <= 0 && playerController.isLevel1 == false)
        {
            myAnimator2.SetTrigger("Dead2");
            StartCoroutine(menuManager.DeadCanvas());
        }

    }
}
