using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public int health;
    private Vector3 spawnPos;
    Rigidbody2D rigidbody2d;
    public float nuttySpeed = 3.0f;
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);


    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spawnPos = transform.position;


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
        animator.SetFloat("nuttySpeed", move.magnitude);

        Vector2 position = rigidbody2d.position;
        /*
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            position.x = position.x + 3.0f * horizontal * Time.deltaTime;
        }
        else if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {

            position.y = position.y + 3.0f * vertical * Time.deltaTime;
        }*/
        position = position + move * nuttySpeed * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    void respawnNutty()
    {

        rigidbody2d.MovePosition(spawnPos);
    }
}
