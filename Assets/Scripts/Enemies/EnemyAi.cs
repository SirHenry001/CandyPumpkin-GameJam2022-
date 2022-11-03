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
    public float fleeSpeed;
    public float aggroRange;
    public float stopRange;
    public float attackTimer;

    // VECTOR WHICH NEEDED FOR MOVEMENT
    public Vector2 movement;

    //VARTIABELS FOR COMPONENTS
    public Rigidbody2D myRigidbody;
    public Animator myAnimator;

    //VARIABLES FOR BOOLEANS
    public bool facingRight;
    public bool isFleeing;

    public AudioManager audioManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hit")
        {
            audioManager.PlayHero(0);
            Instantiate(basketPickUp, spawnBasket.transform.position, spawnBasket.transform.rotation);
            //Instantiate(pickUpEffect, spawnBasket.transform.position, spawnBasket.transform.rotation);
            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //TARGET SPECEFIC GAMEOBJECT TO TARGET THAT ENEMY REACTS
        target =  GameObject.Find("TargetToEnemy").transform;

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponentInChildren<Animator>();
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
        if (distToPlayer < aggroRange && distToPlayer > stopRange && isFleeing == false)
        {
            Approach(movement);
        }

        //ENEMY COMES TOO CLOSE, IT STOPS
        if (distToPlayer < stopRange && isFleeing == false)
        {
            StopChasing();
        }

    }

    public void Approach(Vector2 direction)
    {

        attackTimer = 0;

        //ENEMY MOVE TOWARDS PLAYER X AND Y AXES
        myRigidbody.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));

        // ENEMY IS ON THE LEFT SIDE, TURN LEFT
        if (transform.position.x < target.position.x)
        {
            facingRight = !facingRight;
            transform.localScale = new Vector2(-1, 1);

        }
        // ENEMY IS ON THE RIGHT SIDE, TURN RIGHT
        else
        {
            facingRight = !facingRight;
            transform.localScale = new Vector2(1, 1);

        }

        myAnimator.SetBool("Walking", true);

    }

    public void StopChasing()
    {

        // STARTS THE ATTACKTIMER AND STOPS THE PLAYER TO IDLEANIMATION MODE IN FRONT OF THE PLAYER
        attackTimer += Time.deltaTime;
        myAnimator.SetBool("Walking", false);
        myAnimator.SetBool("Hit", false);
        myRigidbody.velocity = Vector2.zero;

        if (attackTimer > 0.7f) // WHEN ATTACKTIMER REACHES ONE SECOND, ENEMY GOES TO ATTTACK MODE
        {
            StartCoroutine(Attack());
        }
    }

    public IEnumerator Attack()
    {
        //ATTACKTIMER RESET AND ENEMY ANIMATION SET ACTIVE AN ENEMY HIT PLAYER
        //attackTimer = 0;
        myAnimator.SetBool("Hit", true);
        
        yield return new WaitForSeconds(0.5f);
        
        isFleeing = true;
        yield return new WaitForSeconds(0.2f);
        myAnimator.SetBool("Hit", false);

        if (transform.position.x < target.position.x)
        {
            myRigidbody.velocity = new Vector2(-fleeSpeed, myRigidbody.velocity.y);
        }

        if (transform.position.x > target.position.x)
        {
            myRigidbody.velocity = new Vector2(fleeSpeed, myRigidbody.velocity.y);
        }

        yield return new WaitForSeconds(0.5f);
        isFleeing = false;

    }

}
