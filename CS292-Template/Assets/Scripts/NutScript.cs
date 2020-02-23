using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collidedWith)
    {
        if (collidedWith.gameObject.tag == "waterColliders" )
        {
            gameObject.transform.position=new Vector3(Random.Range(-2.2f, 2.3f), Random.Range(-3.3f, 4.2f));
        }
        if (collidedWith.gameObject.tag == "Player")
        {   
            UnityEngine.Debug.Log("nutty collided with nuts");
            ScoreScript.scoreValue += 50;
            SoundMangerScript.PlaySound("nutCollect");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
