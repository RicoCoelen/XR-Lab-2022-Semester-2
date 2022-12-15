using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MInimapManager : MonoBehaviour
{
    public SteamVR_Action_Boolean MinimapTriggerAction;
    public SteamVR_Input_Sources AllDevices = SteamVR_Input_Sources.Any;
    public Canvas minimap;
    public bool minimapIsOn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        //
        minimapIsOn = MinimapTriggerAction.GetState(AllDevices);
        if (minimapIsOn == true)
        {
            minimap.transform.gameObject.SetActive(true);
        }
        else
        {
            minimap.transform.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void MinimapTriggerLogic()
    {

    }
}
