using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class InputManager : MonoBehaviour
{
    public SteamVR_Action_Boolean MenuAction;
    public SteamVR_Action_Boolean PaletteAction;
    public bool minimapIsOn;

    public Canvas minimap;
    public GameObject palette;

    public bool menuButton;
    public bool paletteButton;

    public bool runonceLeft = false;
    public bool runonceRight = false;

    public GameObject playerHead;

    // Start is called before the first frame update
    void Awake()
    {
        if(!palette)
        {
            palette = GameObject.Find("Artist Palette 3D");
        }

       DontDestroyOnLoad(this.gameObject);
    }

    private void FixedUpdate()
    {
        menuButton = MenuAction.GetState(SteamVR_Input_Sources.Any);
        paletteButton = PaletteAction.GetState(SteamVR_Input_Sources.Any);

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

        if(paletteButton)
        {
            if (!runonceRight)
            {
                palette.transform.parent = playerHead.transform;
                palette.transform.position = playerHead.transform.position + (-playerHead.transform.up * 0.2f) + (playerHead.transform.forward * 0.5f);
                palette.transform.rotation = playerHead.transform.rotation;
                palette.transform.Rotate(-45.0f, 0.0f, 0.0f, Space.Self);
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

        minimap.transform.gameObject.SetActive(menuButton);
        palette.transform.gameObject.SetActive(paletteButton);
   }


    public void MinimapTriggerLogic()
    {

    }
}
