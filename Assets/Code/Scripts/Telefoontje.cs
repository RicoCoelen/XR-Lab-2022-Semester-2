using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telefoontje : MonoBehaviour
{
    public AudioSource source1;
    //mooie array gemaakt
    public AudioClip[] audioClips;
    private int audioIndex = 0;

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (source1.isPlaying == false && audioIndex < audioClips.Length)
            {
                source1.clip = audioClips[audioIndex];
                source1.Play();

                audioIndex++;
            }
        }
    }
}
