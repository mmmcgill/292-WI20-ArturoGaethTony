using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class oldPlayerScript : MonoBehaviour
{
    public int health;
    private Vector2 spawnPos;
    private int score;
    public static float enemySpeed;
    public static float capTime;
    Rigidbody2D rigidbody2d;
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    public float speed = 15.0f;
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
        capTime = 3.0f;
        health = 5;
        score = 0;
        enemySpeed = 3.0f;
        rigidbody2d = GetComponent<Rigidbody2D>();
        spawnPos = transform.position;
        animator = GetComponent<Animator>();

        //high scores setting   
        highScores = new ArrayList();
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
        /*
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            position.x = position.x + 15.0f * horizontal * Time.deltaTime;
        }
        else if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {

            position.y = position.y + 15.0f * vertical * Time.deltaTime;
        }
        */
        position = position + move * speed * Time.deltaTime;

        rigidbody2d.MovePosition(position);
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

    public void SetNewScore(int newScore)
    {
        highScores.Add(newScore);
        for (int i = 0; i < 5; i++)
        {
            int num = PlayerPrefs.GetInt("score" + i);
            highScores.Add(num);
        }
        highScores.Sort();
        highScores.Reverse();
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt("score" + i, (int)highScores[i]);
        }
        highScores = new ArrayList();
        setScore();
    }

    public void setScore()
    {
        score1.text = PlayerPrefs.GetInt("score" + 0) == 0 ? "" : PlayerPrefs.GetInt("score" + 0) + "";
        score2.text = PlayerPrefs.GetInt("score" + 1) == 0 ? "" : PlayerPrefs.GetInt("score" + 1) + "";
        score3.text = PlayerPrefs.GetInt("score" + 2) == 0 ? "" : PlayerPrefs.GetInt("score" + 2) + "";
        score4.text = PlayerPrefs.GetInt("score" + 3) == 0 ? "" : PlayerPrefs.GetInt("score" + 3) + "";
        score5.text = PlayerPrefs.GetInt("score" + 4) == 0 ? "" : PlayerPrefs.GetInt("score" + 4) + "";
    }


}
