using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audioplayertest : MonoBehaviour
{
    public AudioSource source1;
    public AudioSource source2;
    public AudioSource source3;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            source1.enabled = true;
            if (source1.isPlaying)
            {
                Debug.Log("hi");
                source2.Pause();
                source3.Pause();
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
