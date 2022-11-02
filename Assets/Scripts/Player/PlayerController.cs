using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject character1;
    public GameObject character2;

    //VARIABLES FOR INPUT
    public float horizontalMove;
    public float verticalMove;
    
    //VARIABLES FOR MOVEMENT
    public float xSpeed = 10f;
    public float ySpeed = 6f;

    //VARIABLES FOR JUMP
    public float radius = 0.1f;
    public float jumpPower;
    public Transform feet;
    public Transform collectCollider;
    public Transform jumpPlatform;
    public LayerMask layerMask;
    public LayerMask layerMaskCollect;

    //VARIABLES FOR GAMEOBJECTS IN GAME
    public GameObject hitObject;
    public GameObject itemToCollect;

    public Rigidbody2D myRigidbody;
    public Animator myAnimator;
    public Animator myAnimator2;

    public bool canMove = true;
    [SerializeField] bool canHit = true;
    [SerializeField] bool canJump;
    
    [SerializeField] bool facingRight = true;

    public bool canCollect;

    //BOOLEANS TO CONTROL ANIMATIONS WITH DIFFERENT CHARACTERS IN SAME LEVEL
    public bool isLevel1 = true;

    [Range(0, 1.0f)]
    [SerializeField] float movementSmooth = 0.5f;
    public Vector3 velocity = Vector3.zero;

    public float collectRadius = 0.5f;

    //ACCESS TO OTHERT SCRIPTS
    public GameManager gameManager;


    public void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponentInChildren<Animator>();
        myAnimator2 = GameObject.Find("Character2").GetComponent<Animator>();
    }

    public void Start()
    {
        canMove = true;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Update()
    {
     
        if(isLevel1 == true)
        {
            character1.SetActive(true);
            character2.SetActive(false);
            myAnimator.SetBool("Level1", true);
        }

        if (isLevel1 == false)
        {
            
            character1.SetActive(false);
            character2.SetActive(true);
            myAnimator.SetBool("Level1", false);
        }



        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Return) && canHit == true)
        {
            myRigidbody.velocity = Vector2.zero;
            canMove = false;
            StartCoroutine(Hit());
        }
        
        if(Input.GetButtonDown("Jump") && canCollect)
        {
            Destroy(itemToCollect);
            gameManager.FillCandy();
        }

    }

    public void FixedUpdate()
    {
        Move(horizontalMove, verticalMove, false);

    }

    public void Move(float xMove, float yMove, bool jump)
    {
        if(canMove == true)
        {
            Vector3 targetVelocity = new Vector2(xMove * xSpeed, yMove * ySpeed);

            myRigidbody.velocity = Vector3.SmoothDamp(myRigidbody.velocity, targetVelocity, ref velocity, movementSmooth);

            // FLIP AND ROTATE CHARACTER TO FACE TO RIGHT DIRECTION WHEN MOVING
            if(xMove > 0  && !facingRight)
            {
                Flip();
            }

            else if(xMove < 0 && facingRight)
            {
                Flip();
            }
        }
    }

    public IEnumerator Hit()
    {
        canHit = false;
        //hitObject.SetActive(true);
        myAnimator.SetTrigger("Hit");
        myAnimator2.SetTrigger("Hit");
        yield return new WaitForSeconds(1f);
        canHit = true;
        //hitObject.SetActive(false);
        
        //myAnimator.SetBool("Attack", false);
        canMove = true;


    }

    public void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
}
