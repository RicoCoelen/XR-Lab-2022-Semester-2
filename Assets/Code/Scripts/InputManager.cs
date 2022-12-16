using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class InputManager : MonoBehaviour
{
    public SteamVR_Action_Boolean MenuAction;
    public SteamVR_Input_Sources AllDevices = SteamVR_Input_Sources.Any;
    public bool minimapIsOn;

    public Canvas minimap;
    public GameObject palette;

    public bool leftMenuButton;
    public bool rightMenuButton;

    public bool runonceLeft = false;
    public bool runonceRight = false;

    public GameObject playerHead;

    // Start is called before the first frame update
    void Start()
    {
       palette = GameObject.Find("Artist Palette 3D");
    }

    private void FixedUpdate()
    {
        leftMenuButton = MenuAction.GetState(SteamVR_Input_Sources.LeftHand);
        rightMenuButton = MenuAction.GetState(SteamVR_Input_Sources.RightHand);

        UpdateMenus();
    }

   public void UpdateMenus()
   {
        
        //if (leftMenuButton && !runonceLeft)
        //{
        //    runonceLeft = true;
        //}
        //else
        //{
        //    runonceLeft = false;
        //}

        if(rightMenuButton)
        {
            if (!runonceRight)
            {
                palette.transform.parent = playerHead.transform;
                palette.transform.position = playerHead.transform.position + (-playerHead.transform.up * 0.5f) + (playerHead.transform.forward * 0.5f);
                palette.transform.rotation = playerHead.transform.rotation;
                palette.transform.parent = null;
                runonceRight = true;
            }
            else
            {

            }
        }
        else
        {
            runonceRight = false;
        }

        minimap.transform.gameObject.SetActive(leftMenuButton);
        palette.transform.gameObject.SetActive(rightMenuButton);
   }


    public void MinimapTriggerLogic()
    {

    }
}
