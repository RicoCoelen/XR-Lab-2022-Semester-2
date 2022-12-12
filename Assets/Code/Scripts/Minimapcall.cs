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
    public Collider area4;

    void OnTriggerStay(Collider collider)
    {
        if (gameObject.tag == "Inarea")
        {
            if (collider.tag == "Player" && spraycan.tag == "Pickedup" && paintbrush.tag == "Untagged")
            {
                if (source1.isPlaying == false && audioIndex1 < audioClips1.Length)
                {
                    source1.clip = audioClips1[audioIndex1];
                    source1.Play();

                    audioIndex1++;
                }
            }
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
    private void Update()
    {
        if (paintbucket.tag == "Respawn" && audioIndex2 == audioClips2.Length)
        {
            paintbucket.SetActive(true);
        }
    }
}
