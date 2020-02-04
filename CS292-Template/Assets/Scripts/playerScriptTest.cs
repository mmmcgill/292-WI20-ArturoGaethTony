using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScriptTest : MonoBehaviour
{
    public int health;
    private Vector2 spawnPos;
    private int score;
    static float enemySpeed;
    Rigidbody2D rigidbody2d;
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    public float speed = 15.0f;


    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        score = 0;
        enemySpeed = 3.0f;
        rigidbody2d = GetComponent<Rigidbody2D>();
        spawnPos = transform.position;
        animator = GetComponent<Animator>();


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


    void OnCollisionEnter2D(Collider2D collidedWith)
    {
        UnityEngine.Debug.Log("nutty collided");
        if (collidedWith.gameObject.tag == "enemies")
        {
            respawnNutty();
            //Destroy(gameObject);
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
        health -= 1;
        if (health == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            respawnNutty();
        }
    }

    void reachedGoal()
    {
        score += 100;
        respawnNutty();
        enemySpeed += 0.5f;

    }
}
