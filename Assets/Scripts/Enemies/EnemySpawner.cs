using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] spawner;

    public static EnemySpawner SharedInstance;

    //lista vihollisista unityyn
    public List<GameObject> enemies;
    //peliobjecti unityss‰ mit‰ laitellaan listaan
    public GameObject[] enemy;
    //m‰‰r‰ on montako vihollisia on peliss‰, hallinoidaan unityss‰
    public int enemiesOnGame;

    //access to player collider script where the difficult level changes
    public GameManager gameManager;

    private void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //enemies = new List<GameObject>();

        for (int i = 0; i < enemiesOnGame; i++)
        {
            GameObject temp = Instantiate(enemy[Random.Range(0,2)]);
            temp.SetActive(false);
            enemies.Add(temp);
        }

        InvokeRepeating(nameof(SpawnRepeat), 2f, 2f);
    }

    public void SpawnRepeat()
    {
        if (gameManager.difficultLevel == 1)
        {
            GameObject localEnemy = GetPooledObject();

            if (localEnemy != null)
            {
                localEnemy.transform.position = (spawner[Random.Range(0, 4)].transform.position);
                localEnemy.transform.rotation = (spawner[Random.Range(0, 4)].transform.rotation);
                localEnemy.SetActive(true);
            }
        }

        if (gameManager.difficultLevel == 2)
        {
            GameObject localEnemy = GetPooledObject();

            if (localEnemy != null)
            {
                localEnemy.transform.position = (spawner[Random.Range(0, 6)].transform.position);
                localEnemy.transform.rotation = (spawner[Random.Range(0, 6)].transform.rotation);
                localEnemy.SetActive(true);
            }
        }


    }

    public GameObject GetPooledObject()
    {

        for (int i = 0; i < enemiesOnGame; i++)
        {
            if (!enemies[i].activeInHierarchy)
            {
                return enemies[i];
            }
        }
        return null;
    }
}
