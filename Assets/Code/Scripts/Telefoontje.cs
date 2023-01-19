using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telefoontje : MonoBehaviour
{
    public AudioSource source1;
    //mooie array gemaakt
    public AudioClip[] audioClips;
    private int audioIndex = 0;

    //if the player enters the dialogue area a series of dialogue fragments are played depending on the certain area.
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (source1.isPlaying == false && audioIndex < audioClips.Length)
            {
                Debug.Log("Hi");
                source1.clip = audioClips[audioIndex];
                source1.Play();

                audioIndex++;
            }
        }
    }
}
