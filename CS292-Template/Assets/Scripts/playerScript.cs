using System.Collections;
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
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    public bool didRespawn;

    private Vector2 startTouchPosition, endTouchPosition;
    private mapScript callLater;

    public ArrayList highScores;

    // Start is called before the first frame update
    void Start()
    {
        capTime = 2.5f;
        health = 5;
        enemySpeed = 3.0f;
        endPosition = transform.position;
        distanceToMove = 0.76f;
        moveSpeed = 4.0f;
        rigidbody2d = GetComponent<Rigidbody2D>();
        spawnPos = transform.position;
        animator = GetComponent<Animator>();
        callLater = GameObject.Find("gameTileMap").GetComponent<mapScript>();
        
        
        //high scores setting   
        highScores =new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == endPosition)
        {
            animator.SetFloat("Speed", 0);
        }

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
                animator.SetBool("Hit", false);
                respawnNutty();
            }
        }
        else
        {

            Vector2 position = rigidbody2d.position;

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                startTouchPosition = Input.GetTouch(0).position;
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                endTouchPosition = Input.GetTouch(0).position;
                float FinX = endTouchPosition.x - startTouchPosition.x;
                float FinY = endTouchPosition.y - startTouchPosition.y;
                if (Mathf.Abs(FinX) > Mathf.Abs(FinY))
                {
                    if (FinX > 0)
                        movement("right");
                    else
                        movement("left");
                }
                else
                {
                    if (FinY > 0)
                        movement("up");
                    else
                        movement("down");
                }
                rigidbody2d.MovePosition(Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime));

            }
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

                rigidbody2d.MovePosition(Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime));

            }
        }
    }

    void movement(string direct)
    {
        if (transform.position != endPosition)
        {
            return;
        }
        animator.SetFloat("Speed", 1);

        switch (direct)
        {
            case "up":
                animator.SetFloat("Look Y", 1);
                animator.SetFloat("Look X", 0);
                endPosition = new Vector3(transform.position.x, transform.position.y + distanceToMove, transform.position.z);
                break;

            case "down":
                animator.SetFloat("Look Y", -1);
                animator.SetFloat("Look X", 0);
                endPosition = new Vector3(transform.position.x, transform.position.y - distanceToMove, transform.position.z);
                break;

            case "left":
                animator.SetFloat("Look X", -1);
                animator.SetFloat("Look Y", 0);
                endPosition = new Vector3(transform.position.x - distanceToMove, transform.position.y, transform.position.z);
                break;

            case "right":
                animator.SetFloat("Look X", 1);
                animator.SetFloat("Look Y", 0);
                endPosition = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
                break;

            default:
                return;
        }
    }
            void OnCollisionEnter2D(Collision2D collidedWith)
    {
        UnityEngine.Debug.Log("Nutty Crashed");
        if (collidedWith.gameObject.tag == "enemies")
        {
            UnityEngine.Debug.Log("nutty collided with enemy");
            respawnNutty();
        }

        if (collidedWith.gameObject.tag == "logs")
        {
            UnityEngine.Debug.Log("nutty collided with log");
            endPosition.x = transform.position.x + 2  * Time.deltaTime;
        }

        if (collidedWith.gameObject.tag == "goal")
        {
            UnityEngine.Debug.Log("nutty collided with goal");
            reachedGoal();
        }

        if (collidedWith.gameObject.name == "borderLeft")
        {
            endPosition = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
            rigidbody2d.MovePosition(Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime));
        }

        if (collidedWith.gameObject.name == "borderRight")
        {
            endPosition = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
            rigidbody2d.MovePosition(Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime));
        }

        if (collidedWith.gameObject.name == "borderBottom")
        {
            endPosition = new Vector3(transform.position.x, transform.position.y + 0.1f, endPosition.z);
            rigidbody2d.MovePosition(Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime));
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
        if (isInvincible)
            return;

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
            isInvincible = true;
            invincibleTimer = timeInvincible;
            animator.SetBool("Hit", true);
            //   respawnNutty();
        }
    }

    public void reachedGoal()
    {
        SoundMangerScript.PlaySound("levelClear");
        ScoreScript.scoreValue += 100;
        respawnNutty();
        enemySpeed += 0.5f;
        if (capTime > 0.5f)
        {
            capTime -= 0.1f;
        }
        callLater.generateMap();
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
        capTime = 2.5f;
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
