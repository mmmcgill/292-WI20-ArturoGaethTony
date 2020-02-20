using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnScript : MonoBehaviour
{
    public GameObject[] enemies;
    private int enemyChoice;
    public float nextSpawnTime;
    public float startTime;
    public GameObject log;
    private Vector2 position;
    private GameObject spawning;
    private float timerCap;
    private bool touchingWater;

    // Start is called before the first frame update
    void Start()
    {   
        enemyChoice = Random.Range(0, enemies.Length);
        startTime = 3.0f;
        nextSpawnTime = 0.5f;
        position = transform.position;
        spawning = enemies[enemyChoice];
        timerCap = playerScript.capTime;
        touchingWater = false;

        //prefab slots:, 
        //Eagles should be in slot 0
        // Eagles in slot 1

        if (enemyChoice == 1){
            position.y += .40f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!touchingWater){
            timerCap = playerScript.capTime;
        }
        if (nextSpawnTime <= 0)
        {
           Instantiate(spawning, position, Quaternion.identity);
            nextSpawnTime = Random.Range(0.5f, timerCap);
            
        }
        else
        {
            nextSpawnTime -= Time.deltaTime;
        }
    }
    void OnTriggerEnter2D(Collider2D collidedWith)
    {
        if (collidedWith.gameObject.tag == "waterColliders")
        {
            UnityEngine.Debug.Log("spawner on water");
            spawning = log;
            timerCap = 2.5f;
            position = transform.position;
            touchingWater = true;
        }

    }

    void OnTriggerExit2D(Collider2D collidedWith)
    {
        if (touchingWater)
        {
            UnityEngine.Debug.Log("Spawner No longer in water");
            spawning = enemies[Random.Range(0, enemies.Length)];
            timerCap = playerScript.capTime;
            touchingWater = false;
            if (enemyChoice == 1)
            {
                position.y += .40f;
            }
        }
    }
}
