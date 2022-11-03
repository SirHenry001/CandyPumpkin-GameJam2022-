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

    //SET PLAYER BOUNDARIES TO MOVE
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;

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
    public AudioManager audioManager;
    public HealthScipt healthScript;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HitEnemy")
        {
            audioManager.PlayEnemy(0);
            healthScript.Death();
        }
    }

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
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        healthScript = GetComponent<HealthScipt>();

        gameManager.candyCollected = 0;
        gameManager.candyCollected2 = 0;
        gameManager.difficultLevel = 1;
        gameManager.gameEnd = false;
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
            audioManager.PlayHero(1);
            myRigidbody.velocity = Vector2.zero;
            canMove = false;
            StartCoroutine(Hit());
        }
        
        if(Input.GetButtonDown("Jump") && canCollect)
        {
            audioManager.PlayEffects(1);
            Destroy(itemToCollect);
            gameManager.FillCandy();
        }

    }

    public void FixedUpdate()
    {
        
        Move(horizontalMove, verticalMove, false);
        PlayerBoundaries();

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

    void PlayerBoundaries()
    {
        //SET Y & X AXIS BOUNDARIES FOR MOVEMENT OF THE PLAYER
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY));
    }
}
