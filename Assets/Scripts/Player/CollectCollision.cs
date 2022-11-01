using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCollision : MonoBehaviour
{

    public PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PickUp" )
        {
            playerController.canCollect = true;
            playerController.itemToCollect = collision.gameObject;

            //collision.gameObject.transform.parent = transform;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PickUp")
        {
            playerController.canCollect = false;
            playerController.itemToCollect = null;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
