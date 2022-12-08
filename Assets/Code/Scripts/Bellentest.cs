using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bellentest : MonoBehaviour
{
    public Collider spraycan;
    public Collider callbutton;
    public Collider area4;
    public Collider paintbrush;

    public AudioSource source1;
    //mooie array gemaakt
    public AudioClip[] audioClips;
    private int audioIndex = 0;

    private void OnTriggerStay(Collider collider)
    {
        if (collider = callbutton)
        {
            if (collider = area4)
            {
                if (spraycan.tag = "pickedup")
                {
                    if (source1.isPlaying == false && audioIndex < audioClips.Length)
                    {
                        source1.clip = audioClips[audioIndex];
                        source1.Play();

                        audioIndex++;
                    }
                if (paintbrush.tag = "pickedup")
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

    void OnTriggerEnter(Collider collider)
    {
        if (collider = spraycan)
        {
            spraycan.GetComponent<Renderer>().tag = "pickedup";
        }

        if (collider = paintbrush)
        {
            paintbrush.GetComponent<Renderer>().tag = "pickedup";
        }
    }
}
