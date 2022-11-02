using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScritp : MonoBehaviour
{

    public GameObject bird;
    public GameObject birdSpawnPoint;

    public Rigidbody2D myRigidbody;
    public float power;
    public float time;
    public float collectTime;

    public PlayerController playerController;


    // Start is called before the first frame update
    void Start()
    {

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        myRigidbody = GetComponent<Rigidbody2D>();

        StartCoroutine(Movement());

    }

    // Update is called once per frame
    void Update()
    {
        collectTime += Time.deltaTime;

        if (collectTime >= 2.5f)
        {
            collectTime = 0;
            Instantiate(bird, birdSpawnPoint.transform.position, birdSpawnPoint.transform.rotation);
        }

    }

    public IEnumerator Movement()
    {
        myRigidbody.AddForce(Vector2.up * power);
        yield return new WaitForSeconds(time);
        myRigidbody.gravityScale = 0;
        myRigidbody.velocity = Vector2.zero;
    }

}
