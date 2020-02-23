using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutsSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    private int numNuts;
    public GameObject nut;
    void Start()
    {
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
            Vector2 nutPos = new Vector3(Random.Range(-2.2f, 2.3f), Random.Range(-3.3f, 4.2f));
            GameObject nutClone=Instantiate(nut, nutPos, Quaternion.identity);
        }
       
    }
    
}
