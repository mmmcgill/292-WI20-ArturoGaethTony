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

    void Start()
    {
        title = Resources.Load<AudioClip> ("Assets/Sounds/title.wav");
        nutCollect = Resources.Load<AudioClip> ("Nut_collect");
        squirrelHit = Resources.Load<AudioClip> ("squirrel_hit");
        levelClear = Resources.Load<AudioClip> ("level_clear");
        jungleBeat = Resources.Load<AudioClip> ("jungle_beat");
        gameOver = Resources.Load<AudioClip> ("game_over");
        buttonClick = Resources.Load<AudioClip> ("button_click");
        birdWhistling = Resources.Load<AudioClip> ("bird_whistling");
        healthIncrease = Resources.Load<AudioClip> ("health_increase");

        audioSrc = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isTitle){
            audioSrc.PlayOneShot(title);
        }else{
            PlaySound("jungleBeat");
        }
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

    public static void clickSound(){
        audioSrc.PlayOneShot(buttonClick);
    }

    public void SetLevel (float sliderValue){
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) *20);
    }
}
