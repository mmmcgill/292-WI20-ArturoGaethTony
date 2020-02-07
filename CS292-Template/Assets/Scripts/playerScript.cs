﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerScript : MonoBehaviour
{
    public int health;
    private Vector2 spawnPos;
    public static float enemySpeed;
    public static float capTime;
    Rigidbody2D rigidbody2d;
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    [SerializeField] private float distanceToMove;
    [SerializeField] private float moveSpeed;
    private Vector3 endPosition;


    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;
    public GameObject gameUI;
    public GameObject gameOverUI;
    public GameObject noPause;
    public Text score1;
    public Text score2;
    public Text score3;
    public Text score4;
    public Text score5;
    

    public ArrayList highScores;
    // Start is called before the first frame update
    void Start()
    {
        capTime = 4.0f;
        health = 5;
        enemySpeed = 3.0f;
        endPosition = transform.position;
        distanceToMove = 1.0f;
        moveSpeed = 5.0f;
        rigidbody2d = GetComponent<Rigidbody2D>();
        spawnPos = transform.position;
        animator = GetComponent<Animator>();
        
        //high scores setting   
        highScores =new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Move X", move.x);
        animator.SetFloat("Move Y", move.y);
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        Vector2 position = rigidbody2d.position;

        if (rigidbody2d.velocity.magnitude == 0)
        {
            if (Input.GetKeyDown(KeyCode.A)) //Left
            {
                movement("left");
            }
            if (Input.GetKeyDown(KeyCode.D)) //Right
            {
                movement("right");
            }
            if (Input.GetKeyDown(KeyCode.W)) //Up
            {
                movement("up");
            }
            if (Input.GetKeyDown(KeyCode.S)) //Down
            {
                movement("down");
            }
            /**
         if (Input.touchCount > 0) {
         Input.GetTouch(0).phase == TouchPhase.Moved; 

             // Get movement of the finger since last frame
             var touchDeltaPosition = Input.GetTouch(0).deltaPosition;

             if (touchDeltaPosition.x > 1)
             {
                 movement("right");
             }
             else if (touchDeltaPosition.x < 1)
             {
                 movement("left");
             }

             else if (touchDeltaPosition.y > 1)
             {
                 movement("up");
             }

             else if (touchDeltaPosition.y < 1)
             {
                movement("down");
             } 
     }
*/
            rigidbody2d.MovePosition(Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime));

        }
    }

    void movement(string direct)
    {
        switch (direct)
        {
            case "up":
                endPosition = new Vector3(endPosition.x, endPosition.y + distanceToMove, endPosition.z);
                break;

            case "down":
                endPosition = new Vector3(endPosition.x, endPosition.y - distanceToMove, endPosition.z);
                break;

            case "left":
                endPosition = new Vector3(endPosition.x - distanceToMove, endPosition.y, endPosition.z);
                break;

            case "right":
                endPosition = new Vector3(endPosition.x + distanceToMove, endPosition.y, endPosition.z);
                break;

            default:
                return;

        }
    }
            void OnCollisionEnter2D(Collision2D collidedWith)
    {
        if (collidedWith.gameObject.tag == "enemies")
        {
            UnityEngine.Debug.Log("nutty collided with enemy");
            respawnNutty();
            //Destroy(gameObject);
        }

        if (collidedWith.gameObject.tag == "goal")
        {
            UnityEngine.Debug.Log("nutty collided with goal");
            reachedGoal();
        }

    }
    public void respawnNutty()
    {

        endPosition = spawnPos;
        UnityEngine.Debug.Log("nuttyRespawned");
        UnityEngine.Debug.Log(spawnPos);
        transform.position = spawnPos;
    }

    public void damage()
    {
        if (health == 5)
            heart1.SetActive(false);
        if (health == 4)
            heart2.SetActive(false);
        if (health == 3)
            heart3.SetActive(false);
        if (health == 2)
            heart4.SetActive(false);
        if (health == 1)
            heart5.SetActive(false);

        health -= 1;
        SoundMangerScript.PlaySound("squirrelHit");

        if (health == 0)
        {    
            SoundMangerScript.PlaySound("gameOver");
            gameOverUI.SetActive(true);
            noPause.SetActive(false);
            gameObject.SetActive(false);
            enemySpeed = 3.0f;
            SetNewScore(ScoreScript.scoreValue);
        }
        else
        {
            respawnNutty();
        }
    }

    public void reachedGoal()
    {
        SoundMangerScript.PlaySound("levelClear");
        ScoreScript.scoreValue += 100;
        respawnNutty();
        enemySpeed += 0.5f;
        if (capTime > 1.0f)
        {
            capTime -= 0.1f;
        }

    }

    public void healNutty()
    {
        health = 5;
        heart1.SetActive(true);
        heart2.SetActive(true);
        heart3.SetActive(true);
        heart4.SetActive(true);
        heart5.SetActive(true);
    }

    public void resetEnemySpeed()
    {
        enemySpeed = 3.0f;
    }

    public void resetScore()
    {
        enemySpeed = 3.0f;
        capTime = 3.0f;
        ScoreScript.scoreValue = 0;
    }

    public void SetNewScore(int newScore){
        highScores.Add(newScore);
        for(int i=0; i<5; i++){
            int num =PlayerPrefs.GetInt("score"+i);
            highScores.Add(num);
        }
        highScores.Sort();
        highScores.Reverse();
        for(int i=0; i<5; i++){
            PlayerPrefs.SetInt("score"+i, (int) highScores[i]);
        }
        highScores=new ArrayList();
        setScore();
    }

    public void setScore(){
        score1.text = PlayerPrefs.GetInt("score"+0) == 0 ? "": PlayerPrefs.GetInt("score"+0) +"";
        score2.text = PlayerPrefs.GetInt("score"+1) == 0 ? "": PlayerPrefs.GetInt("score"+1) +"";
        score3.text = PlayerPrefs.GetInt("score"+2) == 0 ? "": PlayerPrefs.GetInt("score"+2) +"";
        score4.text = PlayerPrefs.GetInt("score"+3) == 0 ? "": PlayerPrefs.GetInt("score"+3) +"";
        score5.text = PlayerPrefs.GetInt("score"+4) == 0 ? "": PlayerPrefs.GetInt("score"+4) +"";
    }


}
