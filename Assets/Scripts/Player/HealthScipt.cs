using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScipt : MonoBehaviour
{

    public int health = 5;

    public Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Death()
    {

        health -= 1;

        if (health <= 0)
        {
            myAnimator.SetTrigger("Dead");
        }

    }
}
