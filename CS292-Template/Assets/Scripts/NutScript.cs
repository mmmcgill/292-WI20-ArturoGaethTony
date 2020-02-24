using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutScript : MonoBehaviour
{
    private NutsSpawner nutsSpawner;

    // Start is called before the first frame update
    void Start()
    {
        nutsSpawner = GameObject.Find("Nuts Spawner").GetComponent<NutsSpawner>();
    }
    void OnTriggerEnter2D(Collider2D collidedWith)
    {
        if (collidedWith.gameObject.tag == "waterColliders")
        {
            // Vector2 nutPos = new Vector2(Random.Range(-2.2f, 2.3f), Random.Range(-3.3f, 4.2f));
            // gameObject.transform.localPosition=nutPos;
            Destroy(gameObject);
            nutsSpawner.generateNut();
        }
        if (collidedWith.gameObject.tag == "Player")
        {   
            UnityEngine.Debug.Log("nutty collided with nuts");
            ScoreScript.scoreValue += 50;
            playerScript.nutsCollected+=1;
            SoundMangerScript.PlaySound("nutCollect");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
