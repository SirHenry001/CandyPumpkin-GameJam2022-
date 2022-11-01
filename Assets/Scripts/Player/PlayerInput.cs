using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    //VARIABLES FOR INPUT
    public float horizontalMove;
    public float verticalMove;

    [SerializeField] private PlayerController playerController;

    public Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        myAnimator = GetComponentInChildren<Animator>();
    }


    public void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        myAnimator.SetFloat("Hor", horizontalMove);
        myAnimator.SetFloat("Ver", verticalMove);
    }

    public void FixedUpdate()
    {
        playerController.Move(horizontalMove, verticalMove, false);
    }
}
