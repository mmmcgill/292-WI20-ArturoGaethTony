using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogScript : MonoBehaviour
{
    public int direction;
    public float speed;
    Rigidbody2D rigidbody2d;
    private bool hasNutty = false;
    //Rigidbody2D nuttyBod;

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
        speed = 2;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = rigidbody2d.position;

        position.x = position.x + speed * direction * Time.deltaTime;
        rigidbody2d.MovePosition(position);
        /**
        if ((hasNutty) & (nuttyBod != null))
        {
            Vector2 nuttyPosition = nuttyBod.position;
            nuttyPosition.x = position.x + speed * direction * Time.deltaTime;
            nuttyBod.MovePosition(nuttyPosition);
        }*/
    }

    void OnTriggerEnter2D(Collider2D collidedWith)
    {
        if (collidedWith.gameObject.tag == "destroyEnemey")
        {
            Destroy(gameObject);
        }
        
        if (collidedWith.gameObject.tag == "Player")
        {
            playerScript nuttyScript = collidedWith.gameObject.GetComponent<playerScript>();
            //nuttyBod = collidedWith.gameObject.GetComponent<Rigidbody2D>();
            nuttyScript.onLog(direction, speed);
            nuttyScript.waterSafety();
            hasNutty = true;

        } 

    }

    void OnTriggerExit2D(Collider2D collidedWith)
    {
        if (collidedWith.gameObject.tag == "waterColliders")
        {
            Destroy(gameObject);
        }
        
        if (collidedWith.gameObject.tag == "Player")
        {
            playerScript nuttyScript = collidedWith.gameObject.GetComponent<playerScript>();
            nuttyScript.waterSafety();
            hasNutty = false;
           // nuttyBod = null;

        }
    }
}
