using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnScript : MonoBehaviour
{
    public GameObject[] enemies;
    private int enemyChoice;
    public float nextSpawnTime;
    public float startTime;
    private Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
        enemyChoice = Random.Range(0, enemies.Length);
        startTime = 3.0f;
        nextSpawnTime = 0.3f;
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextSpawnTime <= 0)
        {
           Instantiate(enemies[enemyChoice], position, Quaternion.identity);
            nextSpawnTime = Random.Range(0.3f, playerScript.capTime);
            
        }
        else
        {
            nextSpawnTime -= Time.deltaTime;
        }
    }
}
