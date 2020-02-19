using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogScript : MonoBehaviour
{
    public int direction;
    public float speed;
    Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Start()
    {
        //direction should be changed by the spawner depending on what side it is. Will add method to do this
        bool onLeft = Camera.main.transform.position.x > transform.position.x;
        if (onLeft)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        speed = playerScript.enemySpeed;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = rigidbody2d.position;

        position.x = position.x + speed * direction * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D collidedWith)
    {
        if (collidedWith.gameObject.tag == "destroyEnemey")
        {
            Destroy(gameObject);
        }

        else if (collidedWith.gameObject.tag == "Player")
        {
            playerScript asdfa = collidedWith.gameObject.GetComponent<playerScript>();
            asdfa.damage();
        }

    }
}
