using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStopController : MonoBehaviour
{
    // Start is called before the first frame update

    //This used by SE 

    private AudioSource playingSource;


    void Start()
    {
         playingSource = GetComponent<AudioSource>();
         playingSource.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!playingSource.isPlaying){
            Destroy(this.gameObject);
        }
    }
}
