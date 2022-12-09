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
    public AudioClip[] audioClips1;
    public AudioClip[] audioClips2;
    private int audioIndex = 0;

    void OnTriggerEnter(Collider collider)
    {
        if (collider = spraycan)
        {
            spraycan.GetComponent<Renderer>().tag = "Pickedup";
        }

        if (collider = paintbrush)
        {
            paintbrush.GetComponent<Renderer>().tag = "Pickedup";
        }
    }
    private void OnTriggerStay(Collider collider)
    {
        if (collider = callbutton)
        {
            if (collider = area4)
            {
                if (spraycan.tag == "Pickedup")
                {
                    if (source1.isPlaying == false && audioIndex < audioClips1.Length && paintbrush.tag == "Untagged")
                    {
                        Debug.Log("hi1");
                        source1.clip = audioClips1[audioIndex];
                        source1.Play();

                        audioIndex++;
                    }
                    if (source1.isPlaying == false && audioIndex < audioClips2.Length && paintbrush.tag == "Pickedup")
                    {
                        Debug.Log("hi2");
                        source1.clip = audioClips2[audioIndex];
                        source1.Play();

                        audioIndex++;
                    }
                }
            }
        }
    }
}
