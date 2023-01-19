using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucketfall : MonoBehaviour
{
    public AudioSource source1;
    public AudioClip[] audioClips1;
    public AudioClip[] audioClips2;
    private int audioIndex1 = 0;
    private int audioIndex2 = 0;

    public GameObject paintbucket;
    public Rigidbody _paintbucket;

    //if the player enters the dialogue area a series of dialogue fragments are played depending on the certain area.
    void OnTriggerStay(Collider collider)
    {
        /*if (collider.tag == "Player")*/
        {
            if (paintbucket.tag == "Respawn" && source1.isPlaying == false && audioIndex1 < audioClips1.Length)
            {
                source1.clip = audioClips1[audioIndex1];
                source1.Play();

                audioIndex1++;
            }
            //the paintbucket is spawned with a sound after two seconds after the previous dialogue.
            if (paintbucket.tag == "Respawn" /*&& audioIndex1 == audioClips1.Length*/)
            {
                StartCoroutine(Delay());
            }
        }
        IEnumerator Delay()
        {
            yield return new WaitForSeconds(3);
            paintbucket.SetActive(true);
            yield return new WaitForSeconds(1);
            _paintbucket.isKinematic = false;
            yield return new WaitForSeconds(5);
            source1.clip = audioClips2[audioIndex2];
            source1.Play();

            audioIndex2++;
        }
    }
}
