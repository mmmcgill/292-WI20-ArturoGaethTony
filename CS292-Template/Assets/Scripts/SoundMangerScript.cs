using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMangerScript : MonoBehaviour
{
    public static AudioClip title, nutCollect, squirrelHit, levelClear, jungleBeat, gameOver, buttonClick, birdWhistling, healthIncrease;
    static AudioSource audioSrc; 
    // Start is called before the first frame update
    public AudioMixer mixer;
    public bool isTitle = true;
    bool isClicked ;
    void Start()
    {
        title = Resources.Load<AudioClip> ("Sounds/title");
        nutCollect = Resources.Load<AudioClip> ("Sounds/Nut_collect");
        squirrelHit = Resources.Load<AudioClip> ("Sounds/squirrel_hit");
        levelClear = Resources.Load<AudioClip> ("Sounds/level_clear");
        jungleBeat = Resources.Load<AudioClip> ("Sounds/jungle_beat");
        gameOver = Resources.Load<AudioClip> ("Sounds/game_over");
        buttonClick = Resources.Load<AudioClip> ("Sounds/button_click");
        birdWhistling = Resources.Load<AudioClip> ("Sounds/bird_whistling");
        healthIncrease = Resources.Load<AudioClip> ("Sounds/health_increase");
        if (GetComponent<AudioSource>() == null){
            gameObject.AddComponent<AudioSource>();
        }
        audioSrc = GetComponent<AudioSource>();
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

    public void clickSound(){
        if (GetComponent<AudioSource>() == null){
            gameObject.AddComponent<AudioSource>();
        }
        audioSrc = GetComponent<AudioSource>();
        audioSrc.PlayOneShot(buttonClick);
    }

    public void SetLevel (float sliderValue){
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) *20);
    }
    
    private void setComop(){
        
    }
}
