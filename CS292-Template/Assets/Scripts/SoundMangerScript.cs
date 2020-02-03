using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMangerScript : MonoBehaviour
{
    public static AudioClip title, nutCollect, squirrelHit, levelClear, jungleBeat, gameOver, buttonClick, birdWhistling, healthIncrease;
    static AudioSource audioSrc; 
    // Start is called before the first frame update
    void Start()
    {
        title = Resources.Load<AudioClip> ("title");
        nutCollect = Resources.Load<AudioClip> ("Nut_collect");
        squirrelHit = Resources.Load<AudioClip> ("squirrel_hit");
        levelClear = Resources.Load<AudioClip> ("level_clear");
        jungleBeat = Resources.Load<AudioClip> ("jungle_beat");
        gameOver = Resources.Load<AudioClip> ("game_over");
        buttonClick = Resources.Load<AudioClip> ("button_click");
        birdWhistling = Resources.Load<AudioClip> ("bird_whistling");
        healthIncrease = Resources.Load<AudioClip> ("health_increase");

        audioSrc = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlaySound(string clip){
        switch(clip){
            case "nutCollect":
                audioSrc.PlayOneShot(nutCollect);
                break;
            case "squirrelHit":
                audioSrc.PlayOneShot(squirrelHit);
                break;
            case "levelClear":
                audioSrc.PlayOneShot(levelClear);
                break;
            case "gameOver":
                audioSrc.PlayOneShot(gameOver);
                break;
            case "buttonClick":
                audioSrc.PlayOneShot(buttonClick);
                break;
            case "healthIncrease":
                audioSrc.PlayOneShot(healthIncrease);
                break;
            case "title":
                audioSrc.PlayOneShot(title);
                break;
            case "jungleBeat":
                audioSrc.PlayOneShot(jungleBeat);
                break;
            case "birdWhistling":
                audioSrc.PlayOneShot(birdWhistling);
                break;
        }
    }
}
