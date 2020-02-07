using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour
{
    [SerializeField] private float distanceToMove;
    [SerializeField] private float moveSpeed;
    private bool moveToPoint = false;
    private Vector3 endPosition;
    Rigidbody2D rigidbody2d;


    void Start()
    {
        endPosition = transform.position;
        distanceToMove = 3.0f;
        rigidbody2d = GetComponent<Rigidbody2D>();
        moveSpeed = 3.0f;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) //Left
        {
            endPosition = new Vector3(endPosition.x - distanceToMove, endPosition.y, endPosition.z);
            moveToPoint = true;
            UnityEngine.Debug.Log("Moving");
        }
        if (Input.GetKeyDown(KeyCode.D)) //Right
        {
            endPosition = new Vector3(endPosition.x + distanceToMove, endPosition.y, endPosition.z);
            moveToPoint = true;
            UnityEngine.Debug.Log("Moving");
        }
        if (Input.GetKeyDown(KeyCode.W)) //Up
        {
            endPosition = new Vector3(endPosition.x, endPosition.y + distanceToMove, endPosition.z);
            moveToPoint = true;
            UnityEngine.Debug.Log("Moving");
        }
        if (Input.GetKeyDown(KeyCode.S)) //Down
        {
            endPosition = new Vector3(endPosition.x, endPosition.y - distanceToMove, endPosition.z);
            moveToPoint = true;
            UnityEngine.Debug.Log("Moving");
        }
        rigidbody2d.MovePosition(Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime));

    }

}