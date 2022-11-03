using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{

    public float fallSpeed;
    public float fleeSpeed;

    public float destroyTimer;

    public bool isFalling;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PickUp")
        {
            collision.gameObject.transform.parent = transform;
            isFalling = false;
        }

        if (collision.gameObject.tag == "Player")
        {
            isFalling = false;
        }
    }





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isFalling)
        {
            transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
        }

        if(!isFalling)
        {
            transform.Translate(Vector2.up * fleeSpeed * Time.deltaTime);
            destroyTimer += Time.deltaTime;

            if(destroyTimer >= 1.3f)
            {
                Destroy(gameObject);
            }

        }

    }
}
