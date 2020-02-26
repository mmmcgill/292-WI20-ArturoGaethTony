using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutsSpawner : MonoBehaviour
{

    // Start is called before the first frame update
    public int numNuts;
    public GameObject nut;
    private playerScript nuttyScript;
    private float yBase;
    void Start()
    {
        nuttyScript = GameObject.Find("playerNutty").GetComponent<playerScript>();
        yBase = nuttyScript.spawnPos.y;
        UnityEngine.Debug.Log(yBase);
        generateNuts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateNuts(){
        //x= random -2.2f and 2.3f
        //y= random -3.3f and 4.2f
        numNuts = Random.Range(2,6);
        for(int i=0;i<numNuts;i++){
            Vector2 nutPos = new Vector2(Random.Range(-2.2f, 2.3f), (yBase + Random.Range(-5, 5)*0.755f));
            GameObject nutClone=Instantiate(nut, nutPos, Quaternion.identity);
        }
    }
    public void generateNut(){
        //x= random -2.2f and 2.3f
        //y= random -3.3f and 4.2f
        Vector2 nutPos = new Vector2(Random.Range(-2.2f, 2.3f), Random.Range(-3.3f, 4.2f));
        GameObject nutClone=Instantiate(nut, nutPos, Quaternion.identity);
        
    }
    
}
