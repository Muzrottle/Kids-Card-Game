using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource cowSound;
    public AudioSource lionSound;
    public AudioSource sheepSound;
    public AudioSource duckSound;
    public AudioSource dogSound;
    public AudioSource catSound;

    public AudioSource finishSound;
    public AudioSource mistakeSound;

    public void playAnimalAudio(string tag)
    {
        if (tag == "Cow")
            cowSound.Play();
        else if (tag == "Lion")
            lionSound.Play();
        else if (tag == "Sheep")
            sheepSound.Play();
        else if (tag == "Duck")
            duckSound.Play();
        else if (tag == "Dog")
            dogSound.Play();
        else
            catSound.Play();
    }

    public void playFinishAudio()
    {
        finishSound.Play();
    }

    public void playMistakeAudio()
    {
        mistakeSound.Play();
    }

}
