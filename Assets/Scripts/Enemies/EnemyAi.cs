using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    //TARGET THAT ENEMY MOVEMENT IS TARGETED TO
    public Transform target;

    //VARIABLES FOR ITEM SPAWN AFTER KILLED
    public GameObject spawnBasket;
    public GameObject basketPickUp;
    public GameObject pickUpEffect;

    //VARIABLE FOR HEALTH
    public int health = 1;

    //VARIABLES FOR SPEED & RANGE
    public float speed;
    public float aggroRange;
    public float stopRange;

    // VECTOR WHICH NEEDED FOR MOVEMENT
    public Vector2 movement;

    //VARTIABELS FOR COMPONENTS
    public Rigidbody2D myRigidbody;
    //public Animator myAnimator;

    //VARIABLES FOR BOOLEANS
    public bool facingRight;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hit")
        {
            Instantiate(basketPickUp, spawnBasket.transform.position, spawnBasket.transform.rotation);
            Instantiate(pickUpEffect, spawnBasket.transform.position, spawnBasket.transform.rotation);
            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //TARGET SPECEFIC GAMEOBJECT TO TARGET THAT ENEMY REACTS
        target =  GameObject.Find("Player").transform;

        myRigidbody = GetComponent<Rigidbody2D>();
        //myAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float distToPlayer = Vector2.Distance(transform.position, target.position);
        Vector2 direction = target.position - transform.position;
        direction.Normalize();
        movement = direction;

        //WHEN ENEMY IS ON AGGRO RANGE, IT STARTS TO APPROACH
        if (distToPlayer < aggroRange && distToPlayer > stopRange)
        {
            Approach(movement);
        }

        //ENEMY COMES TOO CLOSE, IT STOPS
        if (distToPlayer < stopRange)
        {
            myRigidbody.velocity = Vector2.zero;
        }

        //myAnimator.SetFloat("Hor", myRigidbody.velocity.x);

    }

    public void Approach(Vector2 direction)
    {
        //ENEMY MOVE TOWARDS PLAYER X AND Y AXES
        myRigidbody.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));

        // ENEMY IS ON THE LEFT SIDE, TURN LEFT
        if (transform.position.x < target.position.x)
        {
            transform.localScale = new Vector2(-1, 1);

        }
        // ENEMY IS ON THE RIGHT SIDE, TURN RIGHT
        else
        {

            transform.localScale = new Vector2(1, 1);

        }

        //myAnimator.SetFloat("Hor", myRigidbody.velocity.x);
    }

}
