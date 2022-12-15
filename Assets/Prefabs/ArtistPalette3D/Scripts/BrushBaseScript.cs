using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;
using TMPro;
using UnityEngine.UI;

interface IBrush
{
    void CheckBrushInput();
}

public class BrushBaseScript : MonoBehaviour
{
    public GameObject indicator;
    public int widthMultiplier = 1;
    public PaletteManagerScript parentScript;
    
    public Material mat
    {
        get{
            return parentScript.currentMaterial;
        }
        set{
            parentScript.currentMaterial = value;
        }
    }

    public Color col
    {
        get
        {
            return parentScript.currentColor;
        }
        set
        {
            parentScript.currentColor = value;
        }
    }

    public Texture2D tex
    {
        get
        {
            return parentScript.currentTexture;
        }
        set
        {
            parentScript.currentTexture = value;
        }
    }

    public int width
    {
        get
        {
            return parentScript.widthMultiplier;
        }
        set
        {
            parentScript.widthMultiplier = value;
        }
    }

    public List<Line> drawnLines
    {
        get
        {
            return parentScript.lines;
        }
        set
        {
            parentScript.lines = value;
        }
    }

    [Header("Input Variables")]
    // public SteamVR_Action_Single TriggerAction; // reference to actionfile 
    public SteamVR_Action_Boolean GripAction; // reference to actionfile 
    public SteamVR_Action_Single DrawTriggerAction; // reference to actionfile 
    public SteamVR_Input_Sources AllDevices = SteamVR_Input_Sources.Any;
    public Hand LeftHand;
    public Hand RightHand;
    public bool grip = false;
    public float trigger = 0f;
    public bool runOnce = false; // use runonce to check if trigger is held down
    public int interval = 5;

    public  virtual void Awake()
    {
        // get right component hand at start
        LeftHand = GameObject.Find("LeftHand").GetComponent<Hand>();
        RightHand = GameObject.Find("RightHand").GetComponent<Hand>();

        parentScript = transform.parent.GetComponent<PaletteManagerScript>();
        
        if (parentScript == null)
            parentScript = GameObject.Find("Artist Palette 3D").GetComponent<PaletteManagerScript>();

        indicator = Instantiate(indicator, TipPosition(transform.gameObject, width), Quaternion.identity);
        indicator.transform.parent = transform;
    }

    public virtual void FixedUpdate()
    {
        // check inputs
        CheckBrushInput(); // do check then paint

        indicator.transform.position = TipPosition(gameObject, width); // fix indicators
        var temp = indicator.transform.localScale;
        indicator.transform.localScale = new Vector3(1, 1, 1);
        indicator.transform.localScale *= (width * 0.1f);
    }

    public virtual void CheckBrushInput()
    {
        trigger = DrawTriggerAction.GetAxis(AllDevices);
        grip = GripAction.GetState(AllDevices);

        // check if held in either hands
        if (LeftHand.currentAttachedObject == this.gameObject || RightHand.currentAttachedObject == this.gameObject)
        {
            // check if trigger is pressed
            if (trigger > 0)
            {
                // reduce point count and update by delay
                if (Time.frameCount % interval == 0)
                {
                    BrushFunctionality();
                }
            }
            else
            {
                runOnce = false;
            }
        }
    }

    public virtual void BrushFunctionality()
    {

    }

    public virtual Vector3 TipPosition(GameObject brush, int multiplier)
    {
        // calculate brush paint area;
        var pos = brush.transform.position;
        pos += -brush.transform.up * (multiplier * 0.03f);
        return pos;
    }
}
