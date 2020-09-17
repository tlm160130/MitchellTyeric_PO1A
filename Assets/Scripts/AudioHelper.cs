using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioHelper 
{
    //static function = We can't have any Instance of our Class
    //No Variables at the top and reference it
    //Everything must be self contained in function
    //Doesn't even need to be in a scene
    //Can be in folder

    public static AudioSource PlayClip2D(AudioClip clip, float volume) //can be void, but don't. It's fire and forget
    {
        //create new audio source
        GameObject audioObject = new GameObject("2DAudio");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        //configure to 2D
        audioSource.clip = clip;
        audioSource.volume = volume;

        audioSource.Play();
        //destroy when it's done
        Object.Destroy(audioObject, clip.length);
        //then return it
        return audioSource;
    }
}
