using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MInimapManager : MonoBehaviour
{
    public SteamVR_Action_Boolean trigger;
    public SteamVR_Input_Sources AllDevices = SteamVR_Input_Sources.Any;
    public Canvas minimap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (trigger.GetState(AllDevices))
        {
            minimap.transform.gameObject.SetActive(true);
        }
        else
        {
            minimap.transform.gameObject.SetActive(false);
        }
    }
}
