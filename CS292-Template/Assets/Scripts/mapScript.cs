using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class mapScript : MonoBehaviour
{
    //size is 83, 99, 1
    public Tilemap gameMap;
    public Tile[] grassTiles;
    public Tile waterTile;
    public Tile[] waterGrassEdgeTop;
    public Tile[] waterGrassEdgeBot;
    public Tile[] waterMudEdgeTop;
    public Tile[] waterMudEdgeBot;
    public Tile[] mudGrassEdgeTop;
    public Tile[] mudGrassEdgeBot;
    public Tile[] mudTiles;
    Vector3Int hi;
    int x = 83;
    int y = 99-22;
    private string[] types = {"grass", "mud", "water"};

    // Start is called before the first frame update
    void Start()
    {
        //UnityEngine.Debug.Log(gameMap.size);
        //int z = 1;
        //hi = gameMap.WorldToCell(transform.position);
        generateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateMap()
    {
        hi = gameMap.origin;
        hi.y = hi.y + 9;
        Tile currTile = grassTiles[Random.Range(0, grassTiles.Length)];
        string currType = "grass";
        string prevType = "grass";
        string nextType = types[Random.Range(0, 3)];
        bool top = false;
        bool bot = false;
        hi.z = 1;
        int iter = 0;
        prevType = "grass";
        for (int i = 0; i < y; i++)
        {
            if (((i+1) % 7) == 0)
            {
                nextType = types[Random.Range(0, 3)];
                //UnityEngine.Debug.Log("next type is ");
                //UnityEngine.Debug.Log(nextType);
                top = true;
                iter = 0;
            }

            else if ((i % 7) == 0)
            {
                bot = true;
                top = false;
                prevType = currType;
                currType = nextType;
                iter = 0;
            }

            if (currType == "water")
            {
                currTile = waterTile;
            }
            hi.x = gameMap.origin.x;
            for (int j = 0; j < x; j++)
            {
                if (top & (currType != "grass"))
                {
                    if ((currType == "water") & (nextType != "water"))
                    {
                        if (nextType == "grass")
                        {
                            currTile = waterGrassEdgeTop[iter];
                        }/**
                        else if (nextType == "mud")
                        {
                            currTile = waterMudEdgeTop[iter];
                        }*/
                        iter++;
                        if (iter == 4) { iter = 0; }  

                    }

                    if ((currType == "mud") & (nextType == "grass"))
                    {
                        currTile = mudGrassEdgeTop[iter];
                        iter++;
                        if (iter == 3) { iter = 0; }
                    }
                }

                if (bot & (currType != "grass") & (prevType != "water"))
                {

                    if (currType == "water")
                    {
                        if (prevType == "grass")
                        {
                            currTile = waterGrassEdgeBot[iter];
                        }/**
                        else if (prevType == "mud")
                        {
                            currTile = waterMudEdgeBot[iter];
                        }*/
                        iter++;
                        if (iter == 4) { iter = 0; }
                    }
                    if ((currType == "mud") & (prevType != "mud"))
                    {
                        currTile = mudGrassEdgeBot[iter];
                        iter++;
                        if (iter == 3) { iter = 0; }
                    }
              

                    
                }
                else if (currType == "grass")
                {
                    currTile = grassTiles[Random.Range(0, grassTiles.Length)];
                }
                else if (currType == "mud")
                {
                    currTile = mudTiles[Random.Range(0, mudTiles.Length)];
                }
                gameMap.SetTile(hi, currTile);
                hi.x += 1;
            }
            bot = false;
            hi.y += 1;
        }
    }
}
