using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bellentest : MonoBehaviour
{
    public AudioSource wall11;
    public AudioSource wall12;
    public AudioSource wall13;
    public AudioSource wall21;
    public AudioSource wall22;
    public AudioSource wall23;
    public AudioSource spraycall;
    public AudioSource brushcall;

    public Collider callbutton;
    public Collider area4;

    private void OnTriggerStay(Collider collider)
    {
        if (collider = callbutton)
        {
            if (collider = area4)
            {
                if (spraycall.enabled == true)
                {
                    wall11.enabled = true;

                    if (wall11.isPlaying == false)
                    {
                        wall12.enabled = true;
                    }

                    if (wall12.isPlaying == false && wall12.enabled == true)
                    {
                        wall13.enabled = true;
                    }
                }

                if (brushcall.enabled == true)
                {
                    wall21.enabled = true;

                    if (wall21.isPlaying == false)
                    {
                        wall22.enabled = true;
                    }

                    if (wall22.isPlaying == false && wall22.enabled == true)
                    {
                        wall23.enabled = true;
                    }
                }
            }
        }
    }
}
