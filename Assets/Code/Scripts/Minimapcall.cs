using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimapcall : MonoBehaviour
{
    public AudioSource source1;
    public AudioClip[] audioClips1;
    public AudioClip[] audioClips2;
    private int audioIndex1 = 0;
    private int audioIndex2 = 0;

    public GameObject spraycan;
    public GameObject paintbrush;
    public GameObject paintbucket;
    public Rigidbody _paintbucket;
    public Collider area4;

    //if the minimap is inside area4, the call button will get the tag to activate the next code, if it leaves area4 the tag will be removed.
    private void OnTriggerEnter(Collider other)
    {
        if (other = area4)
        {
            gameObject.tag = "Inarea";
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other = area4)
        {
            gameObject.tag = "Untagged";
        }
    }

    //if the minimap is in area4, two possible dialigues will be triggered. 
    void OnTriggerStay(Collider collider)
    {
        if (gameObject.tag == "Inarea")
        {
            //this dialogue will be triggered at first, when only the spraycan has been picked up and the callbutton has been pressed. 
            if (collider.tag == "Player" && spraycan.tag == "Pickedup" && paintbrush.tag == "Untagged")
            {
                if (source1.isPlaying == false && audioIndex1 < audioClips1.Length)
                {
                    source1.clip = audioClips1[audioIndex1];
                    source1.Play();

                    audioIndex1++;
                }
            }
            //this dialogue will be triggered next, when also the paintbrush has been picked up. Also, a paintbucket is spawned after the call. 
            if (collider.tag == "Player" && paintbrush.tag == "Pickedup")
            {
                if (source1.isPlaying == false && audioIndex2 < audioClips2.Length)
                {
                    source1.clip = audioClips2[audioIndex2];
                    source1.Play();

                    audioIndex2++;
                    paintbucket.tag = "Respawn";
                }
            }
        }
    }
}
