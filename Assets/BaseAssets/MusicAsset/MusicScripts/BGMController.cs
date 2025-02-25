using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{


    /*https://qiita.com/lycoris102/items/5d5359b2015a8fdebaaa */

    /*
    bool changing = false;

    int musicNumber = 0;

    public float fadeTime;

    public float musicVolume;
    */

    private AudioSource playingSource;


    //public AudioClip[] musicLayout;


    void Awake(){
        playingSource = GetComponent<AudioSource>();
        //playingSource.clip = musicLayout[musicNumber];
    }

    void Start(){
        //playingSource.volume = musicVolume;
    }

    // Update is called once per frame
    void Update(){

    }

    public void Play(){
         playingSource.Play();
    }

    public void Stop(){
         playingSource.Stop();
    }


}
