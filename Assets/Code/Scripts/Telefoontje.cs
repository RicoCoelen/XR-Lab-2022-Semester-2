using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telefoontje : MonoBehaviour
{
    public AudioSource source1;
    public AudioSource source2;
    public AudioSource source3;

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            source1.enabled = true;

            if (source1.isPlaying == false)
            {
                source2.enabled = true;
            }

            if (source2.isPlaying == false && source2.enabled == true)
            {
                source3.enabled = true;
            } 
        }
    }
}
